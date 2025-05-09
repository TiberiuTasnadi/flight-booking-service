// <copyright file="ExternalFlightDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Text.Json.Serialization;

namespace FlightBooking.Application.Models.ExternalApis;

/// <summary>
/// Represents an external flight data in an external API.
/// </summary>
public class ExternalFlightDto
{
    /// <summary>
    /// Gets or sets the flight key.
    /// </summary>
    public string FlightKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flight number.
    /// </summary>
    public string FlightNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the origin.
    /// </summary>
    public string Origin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination.
    /// </summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flight date.
    /// </summary>
    public string FlightDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of passengers prices.
    /// </summary>
    [JsonPropertyName("PaxPrice")]
#pragma warning disable CA1002 // Do not expose generic lists. For serialization purposes
#pragma warning disable CA2227 // Collection properties should be read only. For serialization purposes
    public List<ExternalPaxPriceDto> PaxPrices { get; set; } = new ();
#pragma warning restore CA2227 // Collection properties should be read only
#pragma warning restore CA1002 // Do not expose generic lists
}