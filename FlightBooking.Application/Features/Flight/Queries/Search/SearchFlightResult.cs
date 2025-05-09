// <copyright file="SearchFlightResult.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Features.Flight.Queries.Search;

/// <summary>
/// Search flight result.
/// </summary>
public class SearchFlightResult
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
    /// Gets or sets the passenger prices.
    /// </summary>
    public IEnumerable<PaxPriceQuery> PaxPrices { get; set; } = new List<PaxPriceQuery>();
}