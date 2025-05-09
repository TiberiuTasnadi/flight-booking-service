// <copyright file="PaxTypeDtoValidator.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Flight;
using FlightBooking.Application.Extensions;
using FlightBooking.Domain.Enums;
using FluentValidation;

namespace FlightBooking.Application.Adapter.Validators;

/// <summary>
/// Validator for the PaxTypeDto.
/// </summary>
public class PaxTypeDtoValidator : AbstractValidator<PaxTypeDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaxTypeDtoValidator"/> class.
    /// </summary>
    public PaxTypeDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Passenger type is required.")
            .Must(code => EnumExtensions.FromCode<PaxType>(code).HasValue)
            .WithMessage("Only 'ADT' (Adult) and 'CHD' (Child) are allowed.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
}