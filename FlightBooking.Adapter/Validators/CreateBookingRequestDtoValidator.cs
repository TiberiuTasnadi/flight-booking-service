// <copyright file="CreateBookingRequestDtoValidator.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Booking;
using FluentValidation;

namespace FlightBooking.Application.Adapter.Validators;

/// <summary>
/// Validator for the CreateBookingRequestDto.
/// </summary>
public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequestDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateBookingRequestValidator"/> class.
    /// </summary>
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.FlightKey)
            .NotEmpty().WithMessage("FlightKey is required.");

        RuleFor(x => x.Passengers)
            .NotEmpty().WithMessage("At least one passenger is required.");

        RuleForEach(x => x.Passengers)
            .SetValidator(new PassengerDtoValidator());

        RuleFor(x => x.Contact)
            .NotNull().WithMessage("Contact is required.")
            .SetValidator(new ContactDtoValidator());
    }
}