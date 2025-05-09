// <copyright file="AdapterRegistration.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Adapter.Contracts;
using FlightBooking.Application.Adapter.Features.Booking;
using FlightBooking.Application.Adapter.Features.Flight;
using FlightBooking.Application.Adapter.Mappings;
using FlightBooking.Application.Adapter.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlightBooking.Application.Adapter.Registration;

/// <summary>
/// Adapter registration class.
/// </summary>
public static class AdapterRegistration
{
    /// <summary>
    /// Adds the adapter services to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection updated.</returns>
    public static IServiceCollection AddAdapters(this IServiceCollection services)
    {
        services.AddScoped<IFlightAdapterService, FlightAdapterService>();
        services.AddScoped<IBookingAdapterService, BookingAdapterService>();

        return services;
    }

    /// <summary>
    /// Adds the mappers to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection updated.</returns>
    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(FlightMappingProfile).Assembly);
        services.AddAutoMapper(typeof(BookingMappingProfile).Assembly);

        return services;
    }

    /// <summary>
    /// Adds the validators to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection updated.</returns>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<SearchFlightRequestDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<PaxTypeDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<PassengerDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateBookingRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<ContactDtoValidator>();

        return services;
    }
}