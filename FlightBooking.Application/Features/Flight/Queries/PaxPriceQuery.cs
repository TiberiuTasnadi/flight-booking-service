// <copyright file="PaxPriceQuery.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Features.Flight.Queries;

/// <summary>
/// Passanger price query.
/// </summary>
public class PaxPriceQuery
{
    /// <summary>
    /// Gets or sets the passenger type.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the passenger type.
    /// </summary>
    public decimal Price { get; set; }
}