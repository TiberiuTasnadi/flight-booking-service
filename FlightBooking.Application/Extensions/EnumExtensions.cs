// <copyright file="EnumExtensions.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;

namespace FlightBooking.Application.Extensions;

/// <summary>
/// Extension methods for enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the code (description) of the enum value.
    /// </summary>
    /// <param name="value">The enum to get the description.</param>
    /// <returns>The description of the enum.</returns>
    public static string GetCode(this Enum value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return value.GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()?
                    .GetCustomAttribute<DescriptionAttribute>()?
                    .Description ?? value.ToString();
    }

    /// <summary>
    /// Gets the enum value from the code (description).
    /// </summary>
    /// <typeparam name="TEnum">The enum type.</typeparam>
    /// <param name="code">The description to search for.</param>
    /// <returns>The enum found.</returns>
    public static TEnum? FromCode<TEnum>(string code)
        where TEnum : struct, Enum
    {
        foreach (var field in typeof(TEnum).GetFields())
        {
            if (Attribute.GetCustomAttribute(
                field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attr &&
                attr.Description == code)
            {
                return (TEnum)field.GetValue(null) !;
            }
        }

        return null;
    }
}