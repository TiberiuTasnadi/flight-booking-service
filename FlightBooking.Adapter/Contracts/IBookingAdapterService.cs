// <copyright file="IBookingAdapterService.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Booking;

namespace FlightBooking.Application.Adapter.Contracts;

/// <summary>
/// Interface for booking adapter service.
/// </summary>
public interface IBookingAdapterService
{
    /// <summary>
    /// Creates a booking asynchronously.
    /// </summary>
    /// <param name="request">The request to create a booking.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A DTO with the booking created.</returns>
    Task<CreateBookingResponseDto> CreateAsync(CreateBookingRequestDto request, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a booking asynchronously.
    /// </summary>
    /// <param name="bookingId">The bookingID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A DTO with the booking found.</returns>
    Task<RetrieveBookingResponseDto> RetrieveAsync(string bookingId, CancellationToken cancellationToken);
}
