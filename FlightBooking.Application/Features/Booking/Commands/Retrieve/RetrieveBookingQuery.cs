// <copyright file="RetrieveBookingQuery.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using MediatR;

namespace FlightBooking.Application.Features.Booking.Commands.Retrieve;

/// <summary>
/// Query to retrieve a booking.
/// </summary>
public class RetrieveBookingQuery : IRequest<RetrieveBookingResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RetrieveBookingQuery"/> class.
    /// </summary>
    /// <param name="bookingId">The booking id.</param>
    /// <param name="contactEmail">The contact email.</param>
    public RetrieveBookingQuery(string bookingId)
    {
        BookingId = bookingId;
    }

    /// <summary>
    /// Gets the booking id.
    /// </summary>
    public string BookingId { get; }
}