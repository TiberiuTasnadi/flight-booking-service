// <copyright file="Contact.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities.Base;

namespace FlightBooking.Domain.Entities;

/// <summary>
/// Represents the contact information for a booking.
/// </summary>
public class Contact : BaseEntity
{
    /// <summary>
    /// Gets or sets the first name of the contact person.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the contact person.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the contact person.
    /// </summary>
    public string Email { get; set; } = string.Empty;
}