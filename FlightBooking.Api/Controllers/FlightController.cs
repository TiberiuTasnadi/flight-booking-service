// <copyright file="FlightController.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Adapter.Contracts;
using FlightBooking.Application.DTO.Flight;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Api.Controllers;

/// <summary>
/// Flight controller.
/// </summary>
[Route("api/[controller]")]
[Tags("Booking Management")]
public class FlightController : ControllerBase
{
    private readonly IFlightAdapterService _flightAdapterService;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightController"/> class.
    /// </summary>
    /// <param name="flightAdapterService">The flight adapter service instance.</param>
    public FlightController(IFlightAdapterService flightAdapterService)
    {
        _flightAdapterService = flightAdapterService;
    }

    /// <summary>
    /// Get available flight for a specific date, origin, and destination.
    /// </summary>
    /// <param name="request">Request parameters to get flights.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A list of available flights for the requested parameters.</returns>
    [HttpPost("Search")]
    public async Task<ActionResult<SearchFlightResponseDto>> SearchAsync([FromBody] SearchFlightRequestDto request, CancellationToken cancellationToken)
    {
        return Ok(await _flightAdapterService.SearchAsync(request, cancellationToken).ConfigureAwait(false));
    }
}