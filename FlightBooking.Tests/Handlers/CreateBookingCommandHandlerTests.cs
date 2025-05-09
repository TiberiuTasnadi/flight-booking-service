using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Features.Booking.Commands.Create;
using FlightBooking.Application.Features.Booking.Commands;
using FlightBooking.Application.Models.ExternalApis;
using FlightBooking.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Tests.Handlers;

/// <summary>
/// Tests for the CreateBookingCommandHandler.
/// </summary>
public class CreateBookingCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IFlightApiClient> _flightApiClientMock;
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly CreateBookingCommandHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingCommandHandlerTests"/> class.
    /// </summary>
    public CreateBookingCommandHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _flightApiClientMock = new Mock<IFlightApiClient>();
        _bookingRepositoryMock = new Mock<IBookingRepository>();

        _unitOfWorkMock.Setup(u => u.BookingRepository).Returns(_bookingRepositoryMock.Object);
        _handler = new CreateBookingCommandHandler(_unitOfWorkMock.Object, _flightApiClientMock.Object);
    }

    /// <summary>
    /// Tests that the handler creates a booking when the request is valid.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldCreateBookingWhenValidRequest()
    {
        // Arrange
        var request = new CreateBookingCommand(
            "FK123",
            new List<PassengerCommand>
            {
                new PassengerCommand { Type = "ADT", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            },
            new ContactCommand { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
        );

        var flight = new ExternalFlightDto
        {
            FlightKey = "FK123",
            FlightNumber = "VY123",
            FlightDate = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            Origin = "BCN",
            Destination = "PAR",
            PaxPrices = new List<ExternalPaxPriceDto>
            {
                new ExternalPaxPriceDto { Type = "ADT", Price = 100 }
            }
        };

        _flightApiClientMock
            .Setup(api => api.GetAvailableFlightsAsync())
            .ReturnsAsync(new List<ExternalFlightDto> { flight });

        _bookingRepositoryMock
            .Setup(repo => repo.GetByBookingIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);

        _unitOfWorkMock
            .Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.BookingId.Should().NotBeNullOrEmpty();
        result.FlightNumber.Should().Be(flight.FlightNumber);
        result.Origin.Should().Be(flight.Origin);
        result.Destination.Should().Be(flight.Destination);
        result.TotalPrice.Should().Be(100);

        _flightApiClientMock.Verify(api => api.GetAvailableFlightsAsync(), Times.Once);
        _bookingRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Booking>(), It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws a NotFoundException when the flight does not exist.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldThrowNotFoundExceptioWhenFlightDoesNotExist()
    {
        // Arrange
        var request = new CreateBookingCommand(
            "FK123",
            new List<PassengerCommand>
            {
                new PassengerCommand { Type = "ADT", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            },
            new ContactCommand { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
        );

        _flightApiClientMock
            .Setup(api => api.GetAvailableFlightsAsync())
            .ReturnsAsync(new List<ExternalFlightDto>());

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Selected flight does not exist.");
        _flightApiClientMock.Verify(api => api.GetAvailableFlightsAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws a BusinessRuleException when the flight is in the past.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldThrowBusinessRuleExceptionWhenFlightIsInThePast()
    {
        // Arrange
        var request = new CreateBookingCommand(
            "FK123",
            new List<PassengerCommand>
            {
                new PassengerCommand { Type = "ADT", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            },
            new ContactCommand { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
        );

        var flight = new ExternalFlightDto
        {
            FlightKey = "FK123",
            FlightNumber = "VY123",
            FlightDate = DateTime.UtcNow.AddDays(-1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            Origin = "BCN",
            Destination = "PAR",
            PaxPrices = new List<ExternalPaxPriceDto>
            {
                new ExternalPaxPriceDto { Type = "ADT", Price = 100 }
            }
        };

        _flightApiClientMock
            .Setup(api => api.GetAvailableFlightsAsync())
            .ReturnsAsync(new List<ExternalFlightDto> { flight });

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BusinessRuleException>().WithMessage("Cannot book a flight in the past.");
        _flightApiClientMock.Verify(api => api.GetAvailableFlightsAsync(), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws a BusinessRuleException when there are no adult passengers.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldThrowBusinessRuleExceptionWhenNoAdultPassenger()
    {
        // Arrange
        var request = new CreateBookingCommand(
            "FK123",
            new List<PassengerCommand>
            {
                new PassengerCommand { Type = "CHD", FirstName = "John", LastName = "Doe", BirthDate = new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            },
            new ContactCommand { FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com" }
        );

        var flight = new ExternalFlightDto
        {
            FlightKey = "FK123",
            FlightNumber = "VY123",
            FlightDate = DateTime.UtcNow.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
            Origin = "BCN",
            Destination = "PAR",
            PaxPrices = new List<ExternalPaxPriceDto>
            {
                new ExternalPaxPriceDto { Type = "CHD", Price = 50 }
            }
        };

        _flightApiClientMock
            .Setup(api => api.GetAvailableFlightsAsync())
            .ReturnsAsync(new List<ExternalFlightDto> { flight });

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BusinessRuleException>().WithMessage("Booking must include at least one adult (ADT).");
        _flightApiClientMock.Verify(api => api.GetAvailableFlightsAsync(), Times.Once);
    }
}