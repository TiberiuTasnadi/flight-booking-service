// <copyright file="ExternalApiException.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.Net;

namespace FlightBooking.Application.Exceptions;

/// <summary>
/// Exception thrown when an external API call fails.
/// </summary>
public class ExternalApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalApiException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message.</param>
    public ExternalApiException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalApiException"/> class with a default error message.
    /// </summary>
    public ExternalApiException()
        : base("An external API call failed.")
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalApiException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message.</param>
    /// <param name="innerException">The inner exception reference.</param>
    public ExternalApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalApiException"/> class with a specified error message and the status code.
    /// </summary>
    /// <param name="statusCode">The status code of the exception.</param>
    /// <param name="message">The error message.</param>
    public ExternalApiException(HttpStatusCode statusCode, string message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Gets the status code of the exception.
    /// </summary>
    public HttpStatusCode StatusCode { get; }
}