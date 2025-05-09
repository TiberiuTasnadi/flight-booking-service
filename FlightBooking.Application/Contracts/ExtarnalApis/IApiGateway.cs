// <copyright file="IApiGateway.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

namespace FlightBooking.Application.Contracts.ExtarnalApis;

/// <summary>
/// Interface for API Gateway.
/// </summary>
public interface IApiGateway
{
    /// <summary>
    /// Executes an action asynchronously and returns the result.
    /// </summary>
    /// <typeparam name="T">The type of the return.</typeparam>
    /// <param name="action">The action to execute.</param>
    /// <param name="operationName">The operation name used to print in logs.</param>
    /// <returns>The response of the api client.</returns>
    Task<T> ExecuteAsync<T>(Func<Task<T>> action, string operationName);
}
