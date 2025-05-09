// <copyright file="RetrieveBookingQueryHandler.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Builders;
using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Application.Exceptions;
using FlightBooking.Domain.Entities;
using MediatR;

namespace FlightBooking.Application.Features.Booking.Commands.Retrieve;

/// <summary>
/// Handler for retrieving booking query.
/// </summary>
public class RetrieveBookingQueryHandler : IRequestHandler<RetrieveBookingQuery, RetrieveBookingResult>
{
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Initializes a new instance of the <see cref="RetrieveBookingQueryHandler"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work instance.</param>
    public RetrieveBookingQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Handles the retrieve booking query.
    /// </summary>
    /// <param name="request">The query to retrieve a booking.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The booking found.</returns>
    public async Task<RetrieveBookingResult> Handle(RetrieveBookingQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        Console.WriteLine($"[{DateTime.UtcNow}] Retrieving booking '{request.BookingId}'...");

        var booking = await _unitOfWork.BookingRepository.GetByBookingIdAsync(request.BookingId, cancellationToken).ConfigureAwait(false);

        if (booking is null)
        {
            Console.WriteLine($"[{DateTime.UtcNow}] Booking '{request.BookingId}' not found.");
            throw new NotFoundException($"Booking with ID '{request.BookingId}' not found.");
        }

        return BuildResult(booking);
    }

    /// <summary>
    /// Builds the result from the booking entity.
    /// </summary>
    /// <param name="booking">The booking entity.</param>
    /// <returns>The retrieve booking result.</returns>
    private RetrieveBookingResult BuildResult(Domain.Entities.Booking booking)
    {
        var passengers = booking.Passengers.Select(p =>
            new PassengerCommand { Type = p.Type, FirstName = p.FirstName, LastName = p.LastName, BirthDate = p.BirthDate }).ToList();

        var contact = new ContactCommand
        {
            FirstName = booking.Contact.FirstName,
            LastName = booking.Contact.LastName,
            Email = booking.Contact.Email,
        };

        return new RetrieveBookingResultBuilder()
            .WithBookingId(booking.BookingId)
            .WithFlight(booking.FlightNumber, booking.FlightDate, booking.Origin, booking.Destination)
            .WithPassengers(passengers)
            .WithContact(contact)
            .WithBookingDate(booking.BookingDate)
            .WithTotalPrice(booking.TotalPrice)
            .Build();
    }
}