// <copyright file="AppDbContext.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Persistance.DbContexts;

/// <summary>
/// Application database context.
/// </summary>
public class AppDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AppDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to configure the DBContext.</param>
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the Booking DbSet.
    /// </summary>
    public DbSet<Booking> Bookings { get; set; }

    /// <summary>
    /// Gets or sets the Passengers DbSet.
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Gets or sets the Contacts DbSet.
    /// </summary>
    public DbSet<Contact> Contacts { get; set; }

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        // In a real-world application, you would get the user from the current context
        // For this demo context, we will use a static user
        var user = "SYSTEM";

        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(nameof(BaseEntity.CreatedOn)).CurrentValue = now;
                entry.Property(nameof(BaseEntity.CreatedBy)).CurrentValue = user;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(nameof(BaseEntity.ModifiedOn)).CurrentValue = now;
                entry.Property(nameof(BaseEntity.ModifiedBy)).CurrentValue = user;
            }
        }

        return await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        // Apply entity configurations (FluentApi) from the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}