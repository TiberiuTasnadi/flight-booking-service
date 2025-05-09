// <copyright file="Booking.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities.Base;

namespace FlightBooking.Domain.Entities;

/// <summary>
/// Represents a booking entity.
/// </summary>
public class Booking : BaseEntity
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
    /// Gets or sets the booking date.
    /// </summary>
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Gets or sets the total price of the booking.
    /// </summary>
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets the list of passengers.
    /// </summary>
    public ICollection<Passenger> Passengers { get; } = new List<Passenger>();

    /// <summary>
    /// Gets or sets the contact ID.
    /// </summary>
    public Guid ContactId { get; set; }

    /// <summary>
    /// Gets or sets the contact information.
    /// </summary>
    public Contact Contact { get; set; } = default!;
}