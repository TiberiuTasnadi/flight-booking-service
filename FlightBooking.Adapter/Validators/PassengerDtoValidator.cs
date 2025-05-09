// <copyright file="PassengerDtoValidator.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Booking;
using FlightBooking.Application.Extensions;
using FlightBooking.Domain.Enums;
using FluentValidation;

namespace FlightBooking.Application.Adapter.Validators;

/// <summary>
/// Validator for the PassengerDto.
/// </summary>
public class PassengerDtoValidator : AbstractValidator<PassengerDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PassengerDtoValidator"/> class.
    /// </summary>
    public PassengerDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .Must(code => EnumExtensions.FromCode<PaxType>(code).HasValue)
            .WithMessage("Only 'ADT' and 'CHD' types are allowed.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("BirthDate is required.");
    }
}