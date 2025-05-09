// <copyright file="CreateBookingResult.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Booking;

namespace FlightBooking.Application.Features.Booking.Commands.Create;

/// <summary>
/// Result of the create booking command.
/// </summary>
public class CreateBookingResult
{
    /// <summary>
    /// Gets or sets the booking identifier.
    /// </summary>
    public string BookingId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flight number.
    /// </summary>
    public string FlightNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flight date.
    /// </summary>
    public DateTime FlightDate { get; set; }

    /// <summary>
    /// Gets or sets the origin of the flight.
    /// </summary>
    public string Origin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination of the flight.
    /// </summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of passengers.
    /// </summary>
    public IEnumerable<PassengerCommand> Passengers { get; set; } = new List<PassengerCommand>();

    /// <summary>
    /// Gets or sets the contact information.
    /// </summary>
    public ContactCommand? Contact { get; set; }

    /// <summary>
    /// Gets or sets the booking date.
    /// </summary>
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Gets or sets the total price of the booking.
    /// </summary>
    public decimal TotalPrice { get; set; }
}