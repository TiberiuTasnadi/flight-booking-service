// <copyright file="CreateBookingResultBuilder.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Features.Booking.Commands;
using FlightBooking.Application.Features.Booking.Commands.Create;

namespace FlightBooking.Application.Builders;

/// <summary>
/// Builder for creating a booking result.
/// </summary>
public class CreateBookingResultBuilder
{
    private string _bookingId = string.Empty;
    private string _flightNumber = string.Empty;
    private DateTime _flightDate;
    private string _origin = string.Empty;
    private string _destination = string.Empty;
    private IEnumerable<PassengerCommand> _passengers = new List<PassengerCommand>();
    private ContactCommand? _contact;
    private DateTime _bookingDate;
    private decimal _totalPrice;

    /// <summary>
    /// Builds a booking with the specified booking ID.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The builder.</returns>
    public CreateBookingResultBuilder WithBookingId(string id)
    {
        _bookingId = id;
        return this;
    }

    /// <summary>
    /// Builds a booking with the specified flight details.
    /// </summary>
    /// <param name="number">The number.</param>
    /// <param name="date">The date.</param>
    /// <param name="origin">The origin.</param>
    /// <param name="destination">The destination.</param>
    /// <returns>The builder.</returns>
    public CreateBookingResultBuilder WithFlight(string number, DateTime date, string origin, string destination)
    {
        _flightNumber = number;
        _flightDate = date;
        _origin = origin;
        _destination = destination;
        return this;
    }

    /// <summary>
    /// Adds passengers to the booking.
    /// </summary>
    /// <param name="passengers">The passangers.</param>
    /// <returns>The builder.</returns>
    public CreateBookingResultBuilder WithPassengers(IEnumerable<PassengerCommand> passengers)
    {
        _passengers = passengers;
        return this;
    }

    /// <summary>
    /// Adds a contact to the booking.
    /// </summary>
    /// <param name="contact">The contact.</param>
    /// <returns>The builder.</returns>
    public CreateBookingResultBuilder WithContact(ContactCommand contact)
    {
        _contact = contact;
        return this;
    }

    /// <summary>
    /// Adds the booking date for the booking.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>The builder.</returns>
    public CreateBookingResultBuilder WithBookingDate(DateTime date)
    {
        _bookingDate = date;
        return this;
    }

    /// <summary>
    /// Adds the total price for the booking.
    /// </summary>
    /// <param name="price">The total price.</param>
    /// <returns>The builder.</returns>
    public CreateBookingResultBuilder WithTotalPrice(decimal price)
    {
        _totalPrice = price;
        return this;
    }

    /// <summary>
    /// Builds the create booking result.
    /// </summary>
    /// <returns>The create booking result.</returns>
    public CreateBookingResult Build()
    {
        return new CreateBookingResult
        {
            BookingId = _bookingId,
            FlightNumber = _flightNumber,
            FlightDate = _flightDate,
            Origin = _origin,
            Destination = _destination,
            Passengers = _passengers,
            Contact = _contact,
            BookingDate = _bookingDate,
            TotalPrice = _totalPrice,
        };
    }
}