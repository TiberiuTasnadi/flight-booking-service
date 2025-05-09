// <copyright file="BookingBuilder.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Builders;

/// <summary>
/// Builder for creating a booking.
/// </summary>
public class BookingBuilder
{
    private string _bookingId = string.Empty;
    private string _flightNumber = string.Empty;
    private DateTime _flightDate;
    private string _origin = string.Empty;
    private string _destination = string.Empty;
    private List<Passenger> _passengers = new ();
    private Contact _contact = default!;
    private DateTime _bookingDate;
    private decimal _totalPrice;

    /// <summary>
    /// Builds a booking with the specified booking ID.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The builder.</returns>
    public BookingBuilder WithBookingId(string id)
    {
        _bookingId = id;
        return this;
    }

    /// <summary>
    /// Builds a booking with the specified flight details.
    /// </summary>
    /// <param name="flightNumber">The flight number.</param>
    /// <param name="date">The date.</param>
    /// <param name="origin">The origin.</param>
    /// <param name="destination">The destination.</param>
    /// <returns>The builder.</returns>
    public BookingBuilder WithFlightDetails(string flightNumber, DateTime date, string origin, string destination)
    {
        _flightNumber = flightNumber;
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
    public BookingBuilder WithPassengers(IEnumerable<Passenger> passengers)
    {
        _passengers = passengers.ToList();
        return this;
    }

    /// <summary>
    /// Adds a contact to the booking.
    /// </summary>
    /// <param name="contact">The contact.</param>
    /// <returns>The builder.</returns>
    public BookingBuilder WithContact(Contact contact)
    {
        _contact = contact;
        return this;
    }

    /// <summary>
    /// Builds a booking with the specified booking date.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>The builder.</returns>
    public BookingBuilder WithBookingDate(DateTime date)
    {
        _bookingDate = date;
        return this;
    }

    /// <summary>
    /// Builds a booking with the specified total price.
    /// </summary>
    /// <param name="price">The total price.</param>
    /// <returns>The builder.</returns>
    public BookingBuilder WithTotalPrice(decimal price)
    {
        _totalPrice = price;
        return this;
    }

    /// <summary>
    /// Builds the booking.
    /// </summary>
    /// <returns>The booking.</returns>
    public Booking Build()
    {
        var booking = new Booking
        {
            BookingId = _bookingId,
            FlightNumber = _flightNumber,
            FlightDate = _flightDate,
            Origin = _origin,
            Destination = _destination,
            Contact = _contact,
            BookingDate = _bookingDate,
            TotalPrice = _totalPrice,
        };

        foreach (var passenger in _passengers)
        {
            booking.Passengers.Add(passenger);
        }

        return booking;
    }
}