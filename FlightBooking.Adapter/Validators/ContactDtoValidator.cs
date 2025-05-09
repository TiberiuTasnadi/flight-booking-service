// <copyright file="ContactDtoValidator.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using FlightBooking.Application.DTO.Booking;
using FluentValidation;

namespace FlightBooking.Application.Adapter.Validators;

/// <summary>
/// Validator for the ContactDto.
/// </summary>
public class ContactDtoValidator : AbstractValidator<ContactDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContactDtoValidator"/> class.
    /// </summary>
    public ContactDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Contact first name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Contact last name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Contact email is required.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}