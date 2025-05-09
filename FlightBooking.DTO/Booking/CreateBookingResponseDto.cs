// <copyright file="CreateBookingResponseDto.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.DTO.Booking;

/// <summary>
/// Create booking response DTO.
/// </summary>
public class CreateBookingResponseDto
{
    /// <summary>
    /// Gets or sets the booking identifier.
    /// </summary>
    public string BookingId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flight number.
    /// </summary>
    public string FlightNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the flight date.
    /// </summary>
    public DateTime FlightDate { get; set; }

    /// <summary>
    /// Gets or sets the origin of the flight.
    /// </summary>
    public string Origin { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the destination of the flight.
    /// </summary>
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of passengers.
    /// </summary>
    public IEnumerable<PassengerDto> Passengers { get; set; } = new List<PassengerDto>();

    /// <summary>
    /// Gets or sets the contact information.
    /// </summary>
    public ContactDto? Contact { get; set; }

    /// <summary>
    /// Gets or sets the booking date.
    /// </summary>
    public DateTime BookingDate { get; set; }

    /// <summary>
    /// Gets or sets the total price of the booking.
    /// </summary>
    public decimal TotalPrice { get; set; }
}