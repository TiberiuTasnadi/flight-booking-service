// <copyright file="BookingMappingProfile.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using AutoMapper;
using FlightBooking.Application.DTO.Booking;
using FlightBooking.Application.Features.Booking.Commands;
using FlightBooking.Application.Features.Booking.Commands.Create;
using FlightBooking.Application.Features.Booking.Commands.Retrieve;

namespace FlightBooking.Application.Adapter.Mappings;

/// <summary>
/// Mapping profile for booking-related mappings.
/// </summary>
public class BookingMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookingMappingProfile"/> class.
    /// </summary>
    public BookingMappingProfile()
    {
        CreateMap<CreateBookingRequestDto, CreateBookingCommand>().ReverseMap();
        CreateMap<CreateBookingResult, CreateBookingResponseDto>().ReverseMap();

        CreateMap<RetrieveBookingResult, RetrieveBookingResponseDto>().ReverseMap();

        CreateMap<PassengerCommand, PassengerDto>().ReverseMap();
        CreateMap<ContactCommand, ContactDto>().ReverseMap();
    }
}