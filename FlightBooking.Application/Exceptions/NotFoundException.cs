// <copyright file="NotFoundException.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Exceptions;

/// <summary>
/// Exception thrown when a requested resource is not found.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public NotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a default error message.
    /// </summary>
    public NotFoundException()
        : base("Item not found.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception reference.</param>
    public NotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
