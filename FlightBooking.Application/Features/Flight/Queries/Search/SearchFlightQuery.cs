// <copyright file="SearchFlightQuery.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using MediatR;

namespace FlightBooking.Application.Features.Flight.Queries.Search;

/// <summary>
/// Search flight query.
/// </summary>
public class SearchFlightQuery : IRequest<SearchFlightResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchFlightQuery"/> class.
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="flightDate">The flight date.</param>
    /// <param name="paxTypes">The passanger types.</param>
    public SearchFlightQuery(
        string origin,
        string? destination,
        DateTime flightDate,
        IEnumerable<PaxTypeQuery> paxTypes)
    {
        this.Origin = origin;
        this.Destination = destination;
        this.FlightDate = flightDate;
        this.PaxTypes = paxTypes;
    }

    /// <summary>
    /// Gets the origin of the flight.
    /// </summary>
    public string Origin { get; }

    /// <summary>
    /// Gets the destination of the flight.
    /// </summary>
    public string? Destination { get; }

    /// <summary>
    /// Gets the date of the flight.
    /// </summary>
    public DateTime FlightDate { get; }

    /// <summary>
    /// Gets the passenger types.
    /// </summary>
    public IEnumerable<PaxTypeQuery> PaxTypes { get; }
}