using AutoMapper;
using FlightBooking.Application.Adapter.Features.Booking;
using FlightBooking.Application.DTO.Booking;
using FlightBooking.Application.DTO.Flight;
using FlightBooking.Application.Features.Booking.Commands;
using FlightBooking.Application.Features.Booking.Commands.Create;
using FlightBooking.Application.Features.Booking.Commands.Retrieve;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace FlightBooking.Tests.Adapters;

/// <summary>
/// Tests for the BookingAdapterService.
/// </summary>
public class BookingAdapterServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly Mock<IValidator<CreateBookingRequestDto>> _validatorMock;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingAdapterServiceTests"/> class.
    /// </summary>
    public BookingAdapterServiceTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateBookingRequestDto, CreateBookingCommand>().ReverseMap();
            cfg.CreateMap<CreateBookingResult, CreateBookingResponseDto>().ReverseMap();
            cfg.CreateMap<RetrieveBookingResult, RetrieveBookingResponseDto>().ReverseMap();
            cfg.CreateMap<PassengerCommand, PassengerDto>().ReverseMap();
            cfg.CreateMap<ContactCommand, ContactDto>().ReverseMap();
        });

        _mapper = config.CreateMapper();
        _mediatorMock = new Mock<IMediator>();
        _validatorMock = new Mock<IValidator<CreateBookingRequestDto>>();
    }

    /// <summary>
    /// Tests that the CreateAsync method sends a CreateBookingCommand and returns the result.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task CreateAsyncShouldSendCreateBookingCommandAndReturnResult()
    {
        // Arrange
        var request = new CreateBookingRequestDto
        {
            FlightKey = "FK123",
            Passengers = new List<PassengerDto>
            {
                new PassengerDto { Type = "ADT", FirstName = "Test1", LastName = "Test2", BirthDate = new DateTime(1990, 1, 1) }
            },
            Contact = new ContactDto { FirstName = "Test3", LastName = "Test4", Email = "test@example.com" }
        };

        var expectedResult = new CreateBookingResult
        {
            BookingId = "BK1234"
        };

        _validatorMock
            .Setup(v => v.Validate(request))
            .Returns(new ValidationResult());

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var adapter = new BookingAdapterService(_mediatorMock.Object, _mapper, _validatorMock.Object);

        // Act
        var result = await adapter.CreateAsync(request, CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
        _mediatorMock.Verify(m => m.Send(It.IsAny<CreateBookingCommand>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}