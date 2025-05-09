// <copyright file="BookingConfiguration.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightBooking.Persistance.Configurations;

/// <summary>
/// Configuration for the <see cref="Booking"/> entity.
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    /// <summary>
    /// Configures the entity of type <see cref="Booking"/> for the database context.
    /// </summary>
    /// <param name="builder">The entity builder.</param>
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(b => b.Id);

        builder.Property(b => b.BookingId)
            .IsRequired()
            .HasMaxLength(6);

        builder.HasIndex(b => b.BookingId)
            .IsUnique();

        builder.Property(b => b.FlightNumber).IsRequired();
        builder.Property(b => b.Origin).IsRequired();
        builder.Property(b => b.Destination).IsRequired();
        builder.Property(b => b.FlightDate).IsRequired();
        builder.Property(b => b.BookingDate).IsRequired();
        builder.Property(b => b.TotalPrice).HasColumnType("decimal(10,2)");

        builder.HasOne(b => b.Contact)
            .WithMany()
            .HasForeignKey(b => b.ContactId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}