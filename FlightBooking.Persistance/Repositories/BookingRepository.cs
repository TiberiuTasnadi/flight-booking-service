// <copyright file="BookingRepository.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Domain.Entities;
using FlightBooking.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Persistance.Repositories;

/// <summary>
/// Repository for managing bookings.
/// </summary>
public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingRepository"/> class.
    /// </summary>
    /// <param name="dbContext">The dbContext instance.</param>
    public BookingRepository(AppDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<Booking?> GetByBookingIdAsync(string bookingId, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNullOrEmpty(bookingId);

        return await _dbContext.Bookings
            .Where(b => !b.IsDeleted)
            .Include(b => b.Passengers)
            .Include(b => b.Contact)
            .FirstOrDefaultAsync(b => b.BookingId == bookingId, cancellationToken)
            .ConfigureAwait(false);
    }
}