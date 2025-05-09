// <copyright file="PaxTypeDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Flight;

/// <summary>
/// Passenger type DTO.
/// </summary>
public class PaxTypeDto
{
    /// <summary>
    /// Gets or sets the type of passenger.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of passengers of this type.
    /// </summary>
    public int Quantity { get; set; }
}
