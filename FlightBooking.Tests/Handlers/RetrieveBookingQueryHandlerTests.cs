using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Features.Booking.Commands.Retrieve;
using FlightBooking.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Tests.Handlers;

public class RetrieveBookingQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IBookingRepository> _bookingRepositoryMock;
    private readonly RetrieveBookingQueryHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="RetrieveBookingQueryHandlerTests"/> class.
    /// </summary>
    public RetrieveBookingQueryHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _bookingRepositoryMock = new Mock<IBookingRepository>();
        _unitOfWorkMock.Setup(u => u.BookingRepository).Returns(_bookingRepositoryMock.Object);
        _handler = new RetrieveBookingQueryHandler(_unitOfWorkMock.Object);
    }

    /// <summary>
    /// Tests that the handler returns the correct result when a booking exists.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldReturnRetrieveBookingResultWhenBookingExists()
    {
        // Arrange
        var bookingId = "BK1234";
        var request = new RetrieveBookingQuery(bookingId);

        var booking = new Booking
        {
            BookingId = bookingId,
            FlightNumber = "VY123",
            FlightDate = DateTime.Today,
            Origin = "BCN",
            Destination = "PAR",
            BookingDate = DateTime.UtcNow,
            TotalPrice = 200,
            Contact = new Contact
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com"
            }
        };

        booking.Passengers.Add(new Passenger
        {
            Type = "ADT",
            FirstName = "John",
            LastName = "Doe",
            BirthDate = new DateTime(1990, 1, 1)
        });

        _bookingRepositoryMock
            .Setup(repo => repo.GetByBookingIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(booking);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.BookingId.Should().Be(bookingId);
        result.FlightNumber.Should().Be(booking.FlightNumber);
        result.Origin.Should().Be(booking.Origin);
        result.Destination.Should().Be(booking.Destination);
        result.TotalPrice.Should().Be(booking.TotalPrice);
        result.Passengers.Should().HaveCount(1);
        result.Contact.Should().NotBeNull();
        result.Contact!.Email.Should().Be(booking.Contact.Email);

        _bookingRepositoryMock.Verify(repo => repo.GetByBookingIdAsync(bookingId, It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws NotFoundException when the booking does not exist.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldThrowNotFoundExceptionWhenBookingDoesNotExist()
    {
        // Arrange
        var bookingId = "BK1234";
        var request = new RetrieveBookingQuery(bookingId);

        _bookingRepositoryMock
            .Setup(repo => repo.GetByBookingIdAsync(bookingId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Booking?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Booking with ID '{bookingId}' not found.");

        _bookingRepositoryMock.Verify(repo => repo.GetByBookingIdAsync(bookingId, It.IsAny<CancellationToken>()), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws ArgumentNullException when the request is null.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldThrowArgumentNullExceptionWhenRequestIsNull()
    {
        // Act
        Func<Task> act = async () => await _handler.Handle(null!, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}