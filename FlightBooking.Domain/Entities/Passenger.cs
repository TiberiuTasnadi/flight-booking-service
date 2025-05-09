// <copyright file="Passenger.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities.Base;

namespace FlightBooking.Domain.Entities;

/// <summary>
/// Represents a passenger entity.
/// </summary>
public class Passenger : BaseEntity
{
    /// <summary>
    /// Gets or sets the passanger type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the passenger.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the passenger.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth date of the passenger.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Gets or sets the booking identifier.
    /// </summary>
    public Guid BookingId { get; set; }

    /// <summary>
    /// Gets or sets the booking associated with this passenger.
    /// </summary>
    public Booking Booking { get; set; } = default!;
}