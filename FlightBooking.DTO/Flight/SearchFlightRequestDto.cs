// <copyright file="SearchFlightRequestDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Flight;

/// <summary>
/// Get flight request DTO.
/// </summary>
public class SearchFlightRequestDto
{
    /// <summary>
    /// Gets or sets the origin of the flight.
    /// </summary>
    public string Origin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination of the flight.
    /// </summary>
    public string? Destination { get; set; }

    /// <summary>
    /// Gets or sets the flight date.
    /// </summary>
    public DateTime FlightDate { get; set; }

    /// <summary>
    /// Gets or sets the list of passengers.
    /// </summary>
    public IEnumerable<PaxTypeDto> PaxTypes { get; set; } = new List<PaxTypeDto>();
}