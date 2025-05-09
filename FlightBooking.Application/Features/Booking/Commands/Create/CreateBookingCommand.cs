// <copyright file="CreateBookingCommand.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using MediatR;

namespace FlightBooking.Application.Features.Booking.Commands.Create;

/// <summary>
/// Command to create a booking.
/// </summary>
public class CreateBookingCommand : IRequest<CreateBookingResult>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingCommand"/> class.
    /// </summary>
    /// <param name="flightKey">The flight key.</param>
    /// <param name="passengers">The passangers.</param>
    /// <param name="contact">The contac.</param>
    public CreateBookingCommand(string flightKey, IEnumerable<PassengerCommand> passengers, ContactCommand contact)
    {
        FlightKey = flightKey;
        Passengers = passengers;
        Contact = contact;
    }

    /// <summary>
    /// Gets or sets the flight key.
    /// </summary>
    public string FlightKey { get; set; }

    /// <summary>
    /// Gets or sets the passengers.
    /// </summary>
    public IEnumerable<PassengerCommand> Passengers { get; set; }

    /// <summary>
    /// Gets or sets the contact information.
    /// </summary>
    public ContactCommand Contact { get; set; }
}