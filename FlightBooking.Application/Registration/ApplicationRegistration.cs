// <copyright file="ApplicationRegistration.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Features.Booking.Commands.Create;
using FlightBooking.Application.Features.Booking.Commands.Retrieve;
using FlightBooking.Application.Features.Flight.Queries.Search;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBooking.Application.Registration;

/// <summary>
/// Application registration class.
/// </summary>
public static class ApplicationRegistration
{
    /// <summary>
    /// Adds the application services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection updated.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(SearchFlightQuery).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RetrieveBookingQuery).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBookingCommand).Assembly));

        return services;
    }
}
