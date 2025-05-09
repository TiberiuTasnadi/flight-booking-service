// <copyright file="UnitOfWork.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Contracts.Persistance;
using FlightBooking.Persistance.DbContexts;

namespace FlightBooking.Persistance.UnitOfWorks;

/// <summary>
/// Unit of Work implementation for managing database transactions.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class.
    /// </summary>
    /// <param name="dbContext">The db context.</param>
    /// <param name="bookingRepository">The booking repository.</param>
    public UnitOfWork(AppDbContext dbContext, IBookingRepository bookingRepository)
    {
        _dbContext = dbContext;
        BookingRepository = bookingRepository;
    }

    /// <summary>
    /// Gets the booking repository.
    /// </summary>
    public IBookingRepository BookingRepository { get; }

    /// <inheritdoc/>
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine($"[{DateTime.UtcNow}] Saving changes to database...");

        var result = await _dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        Console.WriteLine($"[{DateTime.UtcNow}] Save completed. Affected rows: {result}");

        return result;
    }
}
