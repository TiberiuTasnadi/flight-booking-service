// <copyright file="SearchFlightQueryHandler.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Globalization;
using AutoMapper;
using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Models.ExternalApis;
using MediatR;

namespace FlightBooking.Application.Features.Flight.Queries.Search;

/// <summary>
/// Search flight query handler.
/// </summary>
public class SearchFlightQueryHandler : IRequestHandler<SearchFlightQuery, SearchFlightResult>
{
    private readonly IFlightApiClient _flightApiClient;
    private readonly IApiGateway _gateway;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="SearchFlightQueryHandler"/> class.
    /// </summary>
    /// <param name="flightApiClient">The flight api client instance.</param>
    /// <param name="gateway">The gateway instance.</param>
    /// <param name="mapper">The mapper instance.</param>
    public SearchFlightQueryHandler(IFlightApiClient flightApiClient, IApiGateway gateway, IMapper mapper)
    {
        _flightApiClient = flightApiClient;
        _gateway = gateway;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the search flight query.
    /// </summary>
    /// <param name="request">The search query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A search flight result.</returns>
    public async Task<SearchFlightResult> Handle(SearchFlightQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var externalFlights = await _gateway.ExecuteAsync(
            () => _flightApiClient.GetAvailableFlightsAsync(), nameof(_flightApiClient.GetAvailableFlightsAsync)).ConfigureAwait(true);

        var matchingFlights = FilterMatchingFlights(externalFlights, request);

        if (matchingFlights.Count == 0)
        {
            throw new BusinessRuleException("No matching flights found.");
        }

        var cheapest = SelectCheapestFlight(matchingFlights, request.PaxTypes);

        return MapToResult(cheapest);
    }

    /// <summary>
    /// Filters the matching flights based on the search criteria.
    /// </summary>
    /// <param name="flights">The flihts to filter.</param>
    /// <param name="request">The search request used to filter.</param>
    /// <returns>The flights that matches the filter.</returns>
    private static List<ExternalFlightDto> FilterMatchingFlights(IEnumerable<ExternalFlightDto> flights, SearchFlightQuery request)
    {
        return flights
            .Where(f =>
                f.Origin.Equals(request.Origin, StringComparison.OrdinalIgnoreCase) &&
                (string.IsNullOrEmpty(request.Destination) || f.Destination.Equals(request.Destination, StringComparison.OrdinalIgnoreCase)) &&
                DateTime.TryParse(f.FlightDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsed) && parsed.Date == request.FlightDate.Date)
            .ToList();
    }

    /// <summary>
    /// Selects the cheapest flight from the list of flights based on the passenger types and their quantities.
    /// </summary>
    /// <param name="flights">The flihts to search for the cheapest.</param>
    /// <param name="paxTypes">The passanger types.</param>
    /// <returns>The cheapes flight from the list.</returns>
    private static ExternalFlightDto SelectCheapestFlight(IEnumerable<ExternalFlightDto> flights, IEnumerable<PaxTypeQuery> paxTypes)
    {
        return flights
            .OrderBy(flight => CalculateTotalPrice(flight, paxTypes))
            .First();
    }

    /// <summary>
    /// Calculates the total price of a flight based on the passenger types and their quantities.
    /// </summary>
    /// <param name="flight">The flight to calculate the price.</param>
    /// <param name="paxTypes">The passanger types.</param>
    /// <returns>The prices of the flight.</returns>
    private static decimal CalculateTotalPrice(ExternalFlightDto flight, IEnumerable<PaxTypeQuery> paxTypes)
    {
        return paxTypes.Sum(pax =>
            flight.PaxPrices
                .Where(p => p.Type == pax.Type)
                .Select(p => p.Price * pax.Quantity)
                .FirstOrDefault());
    }

    /// <summary>
    /// Maps the external flight DTO to the search flight result.
    /// </summary>
    /// <param name="flight">The external flight DTO.</param>
    /// <returns>The search flight result.</returns>
    private SearchFlightResult MapToResult(ExternalFlightDto flight)
    {
        return _mapper.Map<SearchFlightResult>(flight);
    }
}