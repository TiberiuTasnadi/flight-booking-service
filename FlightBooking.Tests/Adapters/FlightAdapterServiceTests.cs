using AutoMapper;
using FlightBooking.Application.DTO.Flight;
using FlightBooking.Application.Features.Flight.Queries.Search;
using FlightBooking.Application.Features.Flight.Queries;
using MediatR;
using Moq;
using FlightBooking.Application.Adapter.Features.Flight;
using FluentValidation;
using FluentValidation.Results;
using FluentAssertions;
using FlightBooking.Application.Models.ExternalApis;

namespace FlightBooking.Tests.Adapters;

/// <summary>
/// Tests for the FlightAdapterService.
/// </summary>
public class FlightAdapterServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IValidator<SearchFlightRequestDto>> _validatorMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightAdapterServiceTests"/> class.
    /// </summary>
    public FlightAdapterServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<SearchFlightRequestDto, SearchFlightQuery>().ReverseMap();
            cfg.CreateMap<SearchFlightResult, SearchFlightResponseDto>().ReverseMap();
            cfg.CreateMap<PaxPriceQuery, PaxPriceDto>().ReverseMap();
            cfg.CreateMap<PaxTypeDto, PaxTypeQuery>().ReverseMap();
            cfg.CreateMap<ExternalFlightDto, SearchFlightResult>().ReverseMap();
            cfg.CreateMap<ExternalPaxPriceDto, PaxPriceQuery>().ReverseMap();
        });

        _mapper = config.CreateMapper();
        _mediatorMock = new Mock<IMediator>();
        _validatorMock = new Mock<IValidator<SearchFlightRequestDto>>();
    }

    /// <summary>
    /// Tests that the SearchAsync method sends a SearchFlightQuery and returns the result.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task SearchAsyncShouldSendSearchFlightQueryAndReturnResult()
    {
        // Arrange
        var request = new SearchFlightRequestDto
        {
            Origin = "BCN",
            Destination = "PAR",
            FlightDate = DateTime.Today,
            PaxTypes = new List<PaxTypeDto>
            {
                new PaxTypeDto { Type = "ADT", Quantity = 1 }
            }
        };

        var expectedResult = new SearchFlightResult
        {
            FlightKey = "FK001",
            FlightNumber = "VY1234",
            FlightDate = DateTime.Today,
            Origin = "BCN",
            Destination = "PAR",
            PaxPrices = new List<PaxPriceQuery>
            {
                new PaxPriceQuery { Type = "ADT", Price = 79.99m }
            }
        };

        _validatorMock
            .Setup(v => v.Validate(request))
            .Returns(new ValidationResult());

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<SearchFlightQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var adapter = new FlightAdapterService(_mediatorMock.Object, _mapper, _validatorMock.Object);

        // Act
        var result = await adapter.SearchAsync(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
        _mediatorMock.Verify(m => m.Send(It.IsAny<SearchFlightQuery>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
