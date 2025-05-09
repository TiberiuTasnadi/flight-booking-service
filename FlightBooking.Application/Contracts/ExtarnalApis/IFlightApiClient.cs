// <copyright file="IFlightApiClient.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Models.ExternalApis;

namespace FlightBooking.Application.Contracts.ExtarnalApis;

/// <summary>
/// Interface for flight external API.
/// </summary>
public interface IFlightApiClient
{
    /// <summary>
    /// Gets the available flights asynchronously.
    /// </summary>
    /// <returns>A list of flights in a DTO structure.</returns>
    Task<IEnumerable<ExternalFlightDto>> GetAvailableFlightsAsync();
}