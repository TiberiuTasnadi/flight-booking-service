// <copyright file="BusinessRuleException.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Exceptions;

/// <summary>
/// Exception thrown when a business rule is violated.
/// </summary>
public class BusinessRuleException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public BusinessRuleException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleException"/> class with a default error message.
    /// </summary>
    public BusinessRuleException()
        : base("A business rule has been violated.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BusinessRuleException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception reference.</param>
    public BusinessRuleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}