// <copyright file="PassangerConfiguration.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.Persistance.Configurations;

/// <summary>
/// Configuration for the <see cref="Passenger"/> entity.
/// </summary>
public class PassangerConfiguration : IEntityTypeConfiguration<Passenger>
{
    /// <summary>
    /// Configures the entity of type <see cref="Passenger"/> for the database context.
    /// </summary>
    /// <param name="builder">The entity builder.</param>
    public void Configure(EntityTypeBuilder<Passenger> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(b => b.Id);

        builder.Property(x => x.Type).IsRequired();

        builder.Property(x => x.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.BirthDate).IsRequired();

        builder.HasOne(p => p.Booking)
            .WithMany(b => b.Passengers)
            .HasForeignKey(p => p.BookingId);
    }
}