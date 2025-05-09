// <copyright file="ApiGateway.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Net;
using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Application.Exceptions;

namespace FlightBooking.Infrastructure.ExternalApis.Gateway;

/// <summary>
/// Gateway for external API interactions.
/// </summary>
public class ApiGateway : IApiGateway
{
    private const int MaxRetries = 3;
    private const int RetryDelay = 300;

    private readonly List<HttpStatusCode> _retryableStatusCodes =
    [
        HttpStatusCode.RequestTimeout,
        HttpStatusCode.ServiceUnavailable,
        HttpStatusCode.GatewayTimeout,
    ];

    /// <inheritdoc/>
    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action, string operationName)
    {
        ArgumentNullException.ThrowIfNull(action);
        int attempt = 0;

        while (true)
        {
            try
            {
                Console.WriteLine($"[{DateTime.UtcNow}] Starting operation '{operationName}'...");

                var result = await action().ConfigureAwait(false);

                Console.WriteLine($"[{DateTime.UtcNow}] Operation '{operationName}' completed successfully.");

                return result;
            }
            catch (ExternalApiException ex) when (_retryableStatusCodes.Contains(ex.StatusCode))
            {
                attempt++;
                Console.WriteLine($"[{DateTime.UtcNow}] Retriable error on '{operationName}' (attempt {attempt}) - StatusCode: {(int)ex.StatusCode}");

                if (attempt >= MaxRetries)
                {
                    Console.WriteLine($"[{DateTime.UtcNow}] Max retry attempts reached for '{operationName}'. Throwing...");
                    throw;
                }

                await Task.Delay(RetryDelay * attempt).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.UtcNow}] Unexpected error in '{operationName}': {ex.Message}");
                throw;
            }
        }
    }
}
