// <copyright file="ExternalPaxPriceDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Models.ExternalApis;

/// <summary>
/// Represents the price of a passenger in an external API.
/// </summary>
public class ExternalPaxPriceDto
{
    /// <summary>
    /// Gets or sets the passenger type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the passenger.
    /// </summary>
    public decimal Price { get; set; }
}