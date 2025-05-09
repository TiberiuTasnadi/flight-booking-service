// <copyright file="SearchFlightRequestDtoValidator.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Flight;
using FluentValidation;

namespace FlightBooking.Application.Adapter.Validators;

/// <summary>
/// Validator for the SearchFlightRequestDto.
/// </summary>
public class SearchFlightRequestDtoValidator : AbstractValidator<SearchFlightRequestDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SearchFlightRequestDtoValidator"/> class.
    /// </summary>
    public SearchFlightRequestDtoValidator()
    {
        RuleFor(x => x.Origin)
            .NotEmpty().WithMessage("Origin is required.");

        // In production environment, we should check if the date is not from past.
        // The data received from the API returns flights from 2023.
        RuleFor(x => x.FlightDate)
            .NotEmpty().WithMessage("FlightDate is required.");

        RuleForEach(x => x.PaxTypes)
            .SetValidator(new PaxTypeDtoValidator());
    }
}