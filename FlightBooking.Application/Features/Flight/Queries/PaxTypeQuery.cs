// <copyright file="PaxTypeQuery.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Features.Flight.Queries;

/// <summary>
/// Passenger type query.
/// </summary>
public class PaxTypeQuery
{
    /// <summary>
    /// Gets or sets the type of passenger.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity of passengers of this type.
    /// </summary>
    public int Quantity { get; set; }
}