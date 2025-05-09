// <copyright file="BookingAdapterService.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using AutoMapper;
using FlightBooking.Application.Adapter.Contracts;
using FlightBooking.Application.DTO.Booking;
using FlightBooking.Application.DTO.Flight;
using FlightBooking.Application.Features.Booking.Commands.Create;
using FlightBooking.Application.Features.Booking.Commands.Retrieve;
using FluentValidation;
using MediatR;

namespace FlightBooking.Application.Adapter.Features.Booking;

/// <summary>
/// Booking adapter service.
/// </summary>
public class BookingAdapterService : IBookingAdapterService
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBookingRequestDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookingAdapterService"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    /// <param name="mapper">The mapper instance.</param>
    /// <param name="validator">The validator instance.</param>
    public BookingAdapterService(
        IMediator mediator,
        IMapper mapper,
        IValidator<CreateBookingRequestDto> validator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<CreateBookingResponseDto> CreateAsync(CreateBookingRequestDto request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);

        var command = _mapper.Map<CreateBookingCommand>(request);
        var result = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

        return _mapper.Map<CreateBookingResponseDto>(result);
    }

    /// <inheritdoc/>
    public async Task<RetrieveBookingResponseDto> RetrieveAsync(string bookingId, CancellationToken cancellationToken)
    {
        var query = new RetrieveBookingQuery(bookingId);
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return _mapper.Map<RetrieveBookingResponseDto>(result);
    }

    /// <summary>
    /// Validates the search flight request.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    private void ValidateRequest(CreateBookingRequestDto request)
    {
        var result = _validator.Validate(request);
        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray());

            throw new Exceptions.ValidationException(errors);
        }
    }
}