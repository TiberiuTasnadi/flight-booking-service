// <copyright file="SearchFlightResponseDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Flight;

/// <summary>
/// Get flight response DTO.
/// </summary>
public class SearchFlightResponseDto
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
    /// Gets or sets the passanger prices.
    /// </summary>
    public IEnumerable<PaxPriceDto> PaxPrices { get; set; } = new List<PaxPriceDto>();
}