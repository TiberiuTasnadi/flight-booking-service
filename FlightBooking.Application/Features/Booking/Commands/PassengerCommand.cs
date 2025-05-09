// <copyright file="PassengerCommand.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Features.Booking.Commands;

/// <summary>
/// Passanger command.
/// </summary>
public class PassengerCommand
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