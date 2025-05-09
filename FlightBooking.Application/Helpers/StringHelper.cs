// <copyright file="StringHelper.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Helpers;

/// <summary>
/// Helper class for string operations.
/// </summary>
public static class StringHelper
{
    /// <summary>
    /// Generates a random string of the specified length.
    /// </summary>
    /// <param name="length">The lenght of the string.</param>
    /// <returns>The random string.</returns>
    public static string RandomString(int length)
    {
        var bytes = new byte[length];
        System.Security.Cryptography.RandomNumberGenerator.Fill(bytes);

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(bytes.Select(b => chars[b % chars.Length]).ToArray());
    }
}
