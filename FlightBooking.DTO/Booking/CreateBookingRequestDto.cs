// <copyright file="CreateBookingRequestDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Booking;

/// <summary>
/// Booking request DTO.
/// </summary>
public class CreateBookingRequestDto
{
    /// <summary>
    /// Gets or sets the flight key.
    /// </summary>
    public string FlightKey { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of passengers.
    /// </summary>
    public IEnumerable<PassengerDto> Passengers { get; set; } = new List<PassengerDto>();

    /// <summary>
    /// Gets or sets the contact information.
    /// </summary>
    public ContactDto Contact { get; set; } = new ContactDto();
}