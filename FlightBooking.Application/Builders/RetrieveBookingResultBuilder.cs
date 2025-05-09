// <copyright file="RetrieveBookingResultBuilder.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Features.Booking.Commands;
using FlightBooking.Application.Features.Booking.Commands.Retrieve;

namespace FlightBooking.Application.Builders;

/// <summary>
/// Builder for creating a booking result.
/// </summary>
public class RetrieveBookingResultBuilder
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
    /// Adds a booking ID to the result.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The builder.</returns>
    public RetrieveBookingResultBuilder WithBookingId(string id)
    {
        _bookingId = id;
        return this;
    }

    /// <summary>
    /// Adds flight details to the result.
    /// </summary>
    /// <param name="flightNumber">The flight number.</param>
    /// <param name="date">The date.</param>
    /// <param name="origin">The origin.</param>
    /// <param name="destination">The destination.</param>
    /// <returns>The builder.</returns>
    public RetrieveBookingResultBuilder WithFlight(string flightNumber, DateTime date, string origin, string destination)
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
    /// <param name="passengers">The passanger list.</param>
    /// <returns>The builder.</returns>
    public RetrieveBookingResultBuilder WithPassengers(IEnumerable<PassengerCommand> passengers)
    {
        _passengers = passengers;
        return this;
    }

    /// <summary>
    /// Adds a contact to the booking.
    /// </summary>
    /// <param name="contact">The contact.</param>
    /// <returns>The builder.</returns>
    public RetrieveBookingResultBuilder WithContact(ContactCommand contact)
    {
        _contact = contact;
        return this;
    }

    /// <summary>
    /// Adds a booking date to the result.
    /// </summary>
    /// <param name="date">The date.</param>
    /// <returns>The builder.</returns>
    public RetrieveBookingResultBuilder WithBookingDate(DateTime date)
    {
        _bookingDate = date;
        return this;
    }

    /// <summary>
    /// Adds a total price to the result.
    /// </summary>
    /// <param name="price">The total price.</param>
    /// <returns>The builder.</returns>
    public RetrieveBookingResultBuilder WithTotalPrice(decimal price)
    {
        _totalPrice = price;
        return this;
    }

    /// <summary>
    /// Builds the retrieve booking result.
    /// </summary>
    /// <returns>The retrieve booking result.</returns>
    public RetrieveBookingResult Build()
    {
        return new RetrieveBookingResult
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