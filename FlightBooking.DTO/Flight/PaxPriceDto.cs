// <copyright file="PaxPriceDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Flight;

/// <summary>
/// Passenger price DTO.
/// </summary>
public class PaxPriceDto
{
    /// <summary>
    /// Gets or sets the type of passenger.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of this type.
    /// </summary>
    public decimal Price { get; set; }
}