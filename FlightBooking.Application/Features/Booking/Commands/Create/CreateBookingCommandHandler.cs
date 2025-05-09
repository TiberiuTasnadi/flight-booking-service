// <copyright file="CreateBookingCommandHandler.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Globalization;
using System.Threading;
using FlightBooking.Application.Builders;
using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Helpers;
using FlightBooking.Application.Models.ExternalApis;
using FlightBooking.Domain.Entities;
using MediatR;

namespace FlightBooking.Application.Features.Booking.Commands.Create;

/// <summary>
/// Command handler for creating a booking.
/// </summary>
public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, CreateBookingResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFlightApiClient _flightApiClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingCommandHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance.</param>
    /// <param name="flightApiClient">The flight api client instance.</param>
    public CreateBookingCommandHandler(
        IUnitOfWork unitOfWork,
        IFlightApiClient flightApiClient)
    {
        _unitOfWork = unitOfWork;
        _flightApiClient = flightApiClient;
    }

    /// <summary>
    /// Handles the create booking command.
    /// </summary>
    /// <param name="request">The create booking command.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A create booking result.</returns>
    public async Task<CreateBookingResult> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Console.WriteLine($"[{DateTime.UtcNow}] Handling CreateBookingCommand...");

        var flight = await GetFlightAsync(request.FlightKey).ConfigureAwait(false);

        ValidateFlightNotInPast(flight);
        ValidateAtLeastOneAdult(request.Passengers);
        ValidateAdultAge(request.Passengers);

        var booking = await CreateBooking(request, flight, cancellationToken).ConfigureAwait(false);

        await _unitOfWork.BookingRepository.AddAsync(booking, cancellationToken).ConfigureAwait(false);
        await _unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        Console.WriteLine($"[{DateTime.UtcNow}] Booking '{booking.BookingId}' created successfully.");

        return CreateResult(request, booking);
    }

    private static CreateBookingResult CreateResult(CreateBookingCommand request, Domain.Entities.Booking booking)
    {
        return new CreateBookingResultBuilder()
            .WithBookingId(booking.BookingId)
            .WithFlight(booking.FlightNumber, booking.FlightDate, booking.Origin, booking.Destination)
            .WithPassengers(request.Passengers)
            .WithContact(request.Contact)
            .WithBookingDate(booking.BookingDate)
            .WithTotalPrice(booking.TotalPrice)
            .Build();
    }

    /// <summary>
    /// Validates that the adult passengers are at least 16 years old.
    /// </summary>
    /// <param name="passengers">The passengers list.</param>
    private static void ValidateAdultAge(IEnumerable<PassengerCommand> passengers)
    {
        var today = DateTime.UtcNow;

        foreach (var p in passengers.Where(p => p.Type == "ADT"))
        {
            var age = today.Year - p.BirthDate.Year;

            if (age < 16)
            {
                throw new BusinessRuleException($"Passenger '{p.FirstName} {p.LastName}' must be at least 16 years old to be an adult.");
            }
        }
    }

    /// <summary>
    /// Validates that the flight date is not in the past.
    /// </summary>
    /// <param name="flight">The flight to validate the date.</param>
    private static void ValidateFlightNotInPast(ExternalFlightDto flight)
    {
        if (DateTime.Parse(flight.FlightDate, CultureInfo.InvariantCulture, DateTimeStyles.None) < DateTime.UtcNow.Date)
        {
            throw new BusinessRuleException("Cannot book a flight in the past.");
        }
    }

    /// <summary>
    /// Validates that at least one adult passenger is included in the booking.
    /// </summary>
    /// <param name="passengers">The passangers of the booking.</param>
    private static void ValidateAtLeastOneAdult(IEnumerable<PassengerCommand> passengers)
    {
        if (!passengers.Any(p => p.Type == "ADT"))
        {
            throw new BusinessRuleException("Booking must include at least one adult (ADT).");
        }
    }

    /// <summary>
    /// Calculates the total price of the booking based on the passengers and flight information.
    /// </summary>
    /// <param name="flight">The selected flight.</param>
    /// <returns>The total of the booking.</returns>
    private static decimal CalculateTotalPrice(ExternalFlightDto flight)
    {
        return flight.PaxPrices.Sum(p => p.Price);
    }

    private async Task<Domain.Entities.Booking> CreateBooking(CreateBookingCommand request, ExternalFlightDto flight, CancellationToken cancellationToken)
    {
        var totalPrice = CalculateTotalPrice(flight);
        var bookingId = await GenerateUniqueBookingIdAsync(cancellationToken).ConfigureAwait(false);

        var passengers = request.Passengers
           .Select(p => new Passenger { Type = p.Type, FirstName = p.FirstName, LastName = p.LastName, BirthDate = p.BirthDate })
           .ToList();

        var contact = new Contact
        {
            FirstName = request.Contact.FirstName,
            LastName = request.Contact.LastName,
            Email = request.Contact.Email,
        };

        return new BookingBuilder()
            .WithBookingId(bookingId)
            .WithFlightDetails(flight.FlightNumber, DateTime.Parse(flight.FlightDate, CultureInfo.InvariantCulture), flight.Origin, flight.Destination)
            .WithPassengers(passengers)
            .WithContact(contact)
            .WithBookingDate(DateTime.UtcNow)
            .WithTotalPrice(totalPrice)
            .Build();
    }

    /// <summary>
    /// Generates a unique booking ID.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The unique Id.</returns>
    private async Task<string> GenerateUniqueBookingIdAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"[{DateTime.UtcNow}] Attempting to generate unique BookingId...");

        string id;

        do
        {
            id = $"BK{StringHelper.RandomString(4)}";
        }
        while (await _unitOfWork.BookingRepository.GetByBookingIdAsync(id, cancellationToken).ConfigureAwait(false) is not null);

        return id;
    }

    /// <summary>
    /// Gets the flight information from the external API.
    /// It has to be done in this class because the extarnal API does not provide a search by flight key.
    /// </summary>
    /// <param name="flightKey">The flight key.</param>
    /// <returns>The selected flight.</returns>
    private async Task<ExternalFlightDto> GetFlightAsync(string flightKey)
    {
        var flights = await _flightApiClient.GetAvailableFlightsAsync().ConfigureAwait(false);
        return flights.FirstOrDefault(f => f.FlightKey == flightKey)
               ?? throw new NotFoundException("Selected flight does not exist.");
    }
}