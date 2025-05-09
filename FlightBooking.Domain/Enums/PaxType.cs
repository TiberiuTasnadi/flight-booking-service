// <copyright file="PaxType.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.ComponentModel;

namespace FlightBooking.Domain.Enums;

/// <summary>
/// Enumeration for passenger types.
/// </summary>
public enum PaxType
{
    /// <summary>
    /// Adult passenger type.
    /// </summary>
    [Description("ADT")]
    Adult,

    /// <summary>
    /// Child passenger type.
    /// </summary>
    [Description("CHD")]
    Child,
}