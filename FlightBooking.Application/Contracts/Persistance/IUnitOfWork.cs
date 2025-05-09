// <copyright file="IUnitOfWork.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Contracts.Persistance;

/// <summary>
/// Unit of Work interface for managing transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Gets the booking repository.
    /// </summary>
    IBookingRepository BookingRepository { get; }

    /// <summary>
    /// Saves all changes made in the context to the database.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of entities afected by the operation.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}