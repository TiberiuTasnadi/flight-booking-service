// <copyright file="FlightApiOptions.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Infrastructure.ExternalApis.Configurations;

/// <summary>
/// Configuration options for the Flight API.
/// </summary>
public class FlightApiOptions
{
    /// <summary>
    /// The section name in the configuration file.
    /// </summary>
    public const string Section = "FlightApi";

    /// <summary>
    /// Gets or sets the base URL for the Flight API.
    /// </summary>
#pragma warning disable CA1056 // URI-like properties should not be strings. The conversion to Uri is done in the client implementation.
    public string BaseUrl { get; set; } = string.Empty;
#pragma warning restore CA1056 // URI-like properties should not be strings
}