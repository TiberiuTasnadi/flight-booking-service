// <copyright file="FlightMappingProfile.cs" company="PonentLabs">
// Copyright (c) PonentLabs. All rights reserved.
// </copyright>

using AutoMapper;
using FlightBooking.Application.DTO.Flight;
using FlightBooking.Application.Features.Flight.Queries;
using FlightBooking.Application.Features.Flight.Queries.Search;
using FlightBooking.Application.Models.ExternalApis;

namespace FlightBooking.Application.Adapter.Mappings;

/// <summary>
/// Mapping profile for flight-related mappings.
/// </summary>
public class FlightMappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FlightMappingProfile"/> class.
    /// </summary>
    public FlightMappingProfile()
    {
        CreateMap<SearchFlightRequestDto, SearchFlightQuery>().ReverseMap();
        CreateMap<SearchFlightResult, SearchFlightResponseDto>().ReverseMap();
        CreateMap<PaxPriceQuery, PaxPriceDto>().ReverseMap();
        CreateMap<PaxTypeDto, PaxTypeQuery>().ReverseMap();
        CreateMap<ExternalFlightDto, SearchFlightResult>().ReverseMap();
        CreateMap<ExternalPaxPriceDto, PaxPriceQuery>().ReverseMap();
    }
}