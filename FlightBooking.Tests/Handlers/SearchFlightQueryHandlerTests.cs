using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Application.Features.Flight.Queries.Search;
using FlightBooking.Application.Features.Flight.Queries;
using FlightBooking.Application.Models.ExternalApis;
using Moq;
using AutoMapper;
using FlightBooking.Application.Exceptions;
using FluentAssertions;
using System.Globalization;

namespace FlightBooking.Tests.Handlers;

/// <summary>
/// Tests for the SearchFlightQueryHandler.
/// </summary>
public class SearchFlightQueryHandlerTests
{
    private readonly Mock<IFlightApiClient> _flightApiClientMock;
    private readonly Mock<IApiGateway> _gatewayMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly SearchFlightQueryHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchFlightQueryHandlerTests"/> class.
    /// </summary>
    public SearchFlightQueryHandlerTests()
    {
        _flightApiClientMock = new Mock<IFlightApiClient>();
        _gatewayMock = new Mock<IApiGateway>();
        _mapperMock = new Mock<IMapper>();
        _handler = new SearchFlightQueryHandler(_flightApiClientMock.Object, _gatewayMock.Object, _mapperMock.Object);
    }

    /// <summary>
    /// Tests that the handler returns the correct result when matching flights exist.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldReturnMappedResultWhenMatchingFlightsExist()
    {
        // Arrange
        var request = new SearchFlightQuery("BCN", "PAR", DateTime.Today, new List<PaxTypeQuery>
        {
            new PaxTypeQuery { Type = "ADT", Quantity = 1 }
        });

        var externalFlights = new List<ExternalFlightDto>
        {
            new ExternalFlightDto
            {
                Origin = "BCN",
                Destination = "PAR",
                FlightDate = DateTime.Today.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                PaxPrices = new List<ExternalPaxPriceDto>
                {
                    new ExternalPaxPriceDto { Type = "ADT", Price = 100 }
                }
            }
        };

        var cheapestFlight = externalFlights[0];
        var expectedResult = new SearchFlightResult
        {
            FlightKey = "FK001",
            FlightNumber = "VY1234",
            FlightDate = DateTime.Today,
            Origin = "BCN",
            Destination = "PAR",
            PaxPrices = new List<PaxPriceQuery>
            {
                new PaxPriceQuery { Type = "ADT", Price = 100 }
            }
        };

        _gatewayMock
            .Setup(g => g.ExecuteAsync(It.IsAny<Func<Task<IEnumerable<ExternalFlightDto>>>>(), "GetAvailableFlightsAsync"))
            .ReturnsAsync(externalFlights);

        _mapperMock
            .Setup(m => m.Map<SearchFlightResult>(cheapestFlight))
            .Returns(expectedResult);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
        _gatewayMock.Verify(g => g.ExecuteAsync(It.IsAny<Func<Task<IEnumerable<ExternalFlightDto>>>>(), "GetAvailableFlightsAsync"), Times.Once);
        _mapperMock.Verify(m => m.Map<SearchFlightResult>(cheapestFlight), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws a BusinessRuleException when no matching flights exist.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task HandleShouldThrowBusinessRuleExceptionWhenNoMatchingFlightsExist()
    {
        // Arrange
        var request = new SearchFlightQuery("BCN", "PAR", DateTime.Today, new List<PaxTypeQuery>
        {
            new PaxTypeQuery { Type = "ADT", Quantity = 1 }
        });

        var externalFlights = new List<ExternalFlightDto>();

        _gatewayMock
            .Setup(g => g.ExecuteAsync(It.IsAny<Func<Task<IEnumerable<ExternalFlightDto>>>>(), "GetAvailableFlightsAsync"))
            .ReturnsAsync(externalFlights);

        // Act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<BusinessRuleException>().WithMessage("No matching flights found.");
        _gatewayMock.Verify(g => g.ExecuteAsync(It.IsAny<Func<Task<IEnumerable<ExternalFlightDto>>>>(), "GetAvailableFlightsAsync"), Times.Once);
    }

    /// <summary>
    /// Tests that the handler throws an ArgumentNullException when the request is null.
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