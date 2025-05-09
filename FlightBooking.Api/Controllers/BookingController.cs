// <copyright file="BookingController.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.Adapter.Contracts;
using FlightBooking.Application.DTO.Booking;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Api.Controllers;

/// <summary>
/// Booking controller.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Booking Management")]
public class BookingController : ControllerBase
{
    private readonly IBookingAdapterService _bookingAdapterService;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingController"/> class.
    /// </summary>
    /// <param name="bookingAdapterService">The booking adapter service.</param>
    public BookingController(IBookingAdapterService bookingAdapterService)
    {
        _bookingAdapterService = bookingAdapterService;
    }

    /// <summary>
    /// Creates a new booking.
    /// </summary>
    /// <param name="request">The request used to create the booking.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The booking created.</returns>
    [HttpPost("Create")]
    public async Task<ActionResult<CreateBookingResponseDto>> CreateAsync([FromBody] CreateBookingRequestDto request, CancellationToken cancellationToken)
    {
        return Ok(await _bookingAdapterService.CreateAsync(request, cancellationToken).ConfigureAwait(false));
    }

    /// <summary>
    /// Retrieves a booking by its identifier.
    /// </summary>
    /// <param name="bookingId">The booking ID to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The booking found by its id.</returns>
    [HttpGet("{bookingId}")]
    public async Task<ActionResult<RetrieveBookingResponseDto>> RetrieveAsync(string bookingId, CancellationToken cancellationToken)
    {
        return Ok(await _bookingAdapterService.RetrieveAsync(bookingId, cancellationToken).ConfigureAwait(false));
    }
}