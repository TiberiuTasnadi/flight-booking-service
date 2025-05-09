// <copyright file="IFlightAdapterService.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Flight;

namespace FlightBooking.Application.Adapter.Contracts;

/// <summary>
/// Interface for flight adapter service.
/// </summary>
public interface IFlightAdapterService
{
    /// <summary>
    /// Searches for a flight asynchronously.
    /// </summary>
    /// <param name="request">The request with the parameters to search.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A DTO with the flight found.</returns>
    Task<SearchFlightResponseDto> SearchAsync(SearchFlightRequestDto request, CancellationToken cancellationToken);
}