// <copyright file="FlightAdapterService.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using AutoMapper;
using FlightBooking.Application.Adapter.Contracts;
using FlightBooking.Application.DTO.Flight;
using FlightBooking.Application.Features.Flight.Queries.Search;
using FluentValidation;
using MediatR;

namespace FlightBooking.Application.Adapter.Features.Flight;

/// <summary>
/// Flight adapter service.
/// </summary>
public class FlightAdapterService : IFlightAdapterService
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IValidator<SearchFlightRequestDto> _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="FlightAdapterService"/> class.
    /// </summary>
    /// <param name="mediator">The mediator instance.</param>
    /// <param name="mapper">The mapper instance.</param>
    /// <param name="validator">The validator instance.</param>
    public FlightAdapterService(
        IMediator mediator,
        IMapper mapper,
        IValidator<SearchFlightRequestDto> validator)
    {
        _mediator = mediator;
        _mapper = mapper;
        _validator = validator;
    }

    /// <inheritdoc/>
    public async Task<SearchFlightResponseDto> SearchAsync(SearchFlightRequestDto request, CancellationToken cancellationToken)
    {
        ValidateRequest(request);

        var query = _mapper.Map<SearchFlightQuery>(request);
        var result = await _mediator.Send(query, cancellationToken).ConfigureAwait(false);

        return _mapper.Map<SearchFlightResponseDto>(result);
    }

    /// <summary>
    /// Validates the search flight request.
    /// </summary>
    /// <param name="request">The request to validate.</param>
    private void ValidateRequest(SearchFlightRequestDto request)
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