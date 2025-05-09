// <copyright file="IBookingRepository.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Contracts.Persistance;

/// <summary>
/// Interface for booking repository.
/// </summary>
public interface IBookingRepository : IRepository<Booking>
{
    /// <summary>
    /// Retrieves a booking by its booking ID.
    /// </summary>
    /// <param name="bookingId">The booking ID.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A booking searched by its id with the passangers and the contact related.</returns>
    Task<Booking?> GetByBookingIdAsync(string bookingId, CancellationToken cancellationToken);
}