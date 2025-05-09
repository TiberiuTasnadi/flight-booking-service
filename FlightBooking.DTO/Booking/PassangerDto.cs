// <copyright file="PassangerDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Booking;

/// <summary>
/// Passenger DTO.
/// </summary>
public class PassengerDto
{
    /// <summary>
    /// Gets or sets the type of passenger.
    /// Possible values are: ADT (Adult), CHD (Child).
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the birth date.
    /// </summary>
    public DateTime BirthDate { get; set; }
}
