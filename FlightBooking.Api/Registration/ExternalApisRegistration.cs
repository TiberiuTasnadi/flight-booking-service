// <copyright file="ExternalApisRegistration.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Contracts.ExtarnalApis;
using FlightBooking.Infrastructure.ExternalApis.Clients;
using FlightBooking.Infrastructure.ExternalApis.Configurations;
using FlightBooking.Infrastructure.ExternalApis.Gateway;

namespace FlightBooking.Api.Registration;

/// <summary>
/// External APIs registration class.
/// </summary>
public static class ExternalApisRegistration
{
    /// <summary>
    /// Adds external API services to the service collection.
    /// The ExternalApi class library, does not have some packages required to register this services.
    /// In this demo context, we are registering the services in the API project.
    /// In a real-world application, this should be done in the ExternalApi class library by addind the required packages.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection updated.</returns>
    public static IServiceCollection AddExternalApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        services.Configure<FlightApiOptions>(configuration.GetSection(FlightApiOptions.Section));

        services.AddHttpClient<IFlightApiClient, FlightApiClient>();

        services.AddScoped<IApiGateway, ApiGateway>();

        return services;
    }
}