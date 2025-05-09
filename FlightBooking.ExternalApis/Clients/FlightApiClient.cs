// <copyright file="FlightApiClient.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Models.ExternalApis;
using FlightBooking.Infrastructure.ExternalApis.Configurations;
using Microsoft.Extensions.Options;

namespace FlightBooking.Infrastructure.ExternalApis.Clients;

/// <summary>
/// Flight API client.
/// </summary>
public class FlightApiClient : IFlightApiClient
{
    // Temporary implementation to read from a local file instead of an external API.
    private readonly HttpClient _httpClient;
    private readonly FlightApiOptions _options;

    private readonly string _filePath;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightApiClient"/> class.
    /// </summary>
    /// <param name="httpClient">The http client instance.</param>
    /// <param name="options">The options instance.</param>
    public FlightApiClient(HttpClient httpClient, IOptions<FlightApiOptions> options)
    {
        ArgumentNullException.ThrowIfNull(options);

        _httpClient = httpClient;
        _options = options.Value;

        _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "flights.json");
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<ExternalFlightDto>> GetAvailableFlightsAsync()
    {
        // Temporary implementation to read from a local file instead of an external API.
        if (!File.Exists(_filePath))
        {
            throw new FileNotFoundException($"Flight data file not found: {_filePath}");
        }

        var content = await File.ReadAllTextAsync(_filePath, CancellationToken.None).ConfigureAwait(false);

        var jsonSerializer = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var flights = JsonSerializer.Deserialize<List<ExternalFlightDto>>(content, jsonSerializer);

        return flights ?? Enumerable.Empty<ExternalFlightDto>();
    }
}