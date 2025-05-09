// <copyright file="ExceptionHandlingMiddleware.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Net;
using FlightBooking.Application.Exceptions;

namespace FlightBooking.Api.Middleware;

/// <summary>
/// Middleware to handle exceptions in the application.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The request delegate.</param>
    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Handles exceptions that occur during the request processing pipeline.
    /// </summary>
    /// <param name="context">The http context.</param>
    /// <returns>The response of the process.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
#pragma warning disable CA1031 // Do not catch general exception types. We need to catch all exceptions to handle them properly.
        try
        {
            Console.WriteLine($"[{DateTime.UtcNow}] Incoming request: {context.Request.Method} {context.Request.Path}");

            await _next(context).ConfigureAwait(false);
        }
        catch (BusinessRuleException ex)
        {
            // Bussines rules exception and Validation exception have the same status code.
            // So, we want to have the same structure as the validation exception handling.
            // Valiadtion exception build the request with a dictionary.
            var errors = new Dictionary<string, string[]> { { "Business", new[] { ex.Message } } };
            await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, "Business rule violated", errors).ConfigureAwait(false);
        }
        catch (ValidationException ex)
        {
            await WriteErrorResponseAsync(context, StatusCodes.Status400BadRequest, "Validation failed", ex.Errors).ConfigureAwait(false);
        }
        catch (ExternalApiException ex)
        {
            await WriteErrorResponseAsync(context, StatusCodes.Status404NotFound, "External API error", new { ex.Message, ex.StatusCode }).ConfigureAwait(false);
        }
        catch (NotFoundException ex)
        {
            await WriteErrorResponseAsync(context, StatusCodes.Status404NotFound, "Resource not found", new { ex.Message }).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await WriteErrorResponseAsync(context, StatusCodes.Status500InternalServerError, "Internal server error", new { ex.Message }).ConfigureAwait(false);
        }
#pragma warning restore CA1031 // Do not catch general exception types
    }

    private static async Task WriteErrorResponseAsync(HttpContext context, int statusCode, string error, object details)
    {
        context.Response.StatusCode = statusCode;

        Console.WriteLine($"[{DateTime.UtcNow}] {error} ({statusCode}): {System.Text.Json.JsonSerializer.Serialize(details)}");

        var response = new
        {
            Error = error,
            Details = details,
        };

        await context.Response.WriteAsJsonAsync(response).ConfigureAwait(false);
    }
}