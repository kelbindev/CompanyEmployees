﻿using AutoMapper;
using Entities.Models;
using Shared.Dto;

namespace CompanyEmployees;

public class MappingProfile : Profile
{
    public MappingProfile() {
        CreateMap<Company, CompanyDto>()
            .ForCtorParam("FullAddress", opt => 
                opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<CompanyForCreationDto, Company>().ReverseMap();
        CreateMap<EmployeeForCreationDto, Employee>().ReverseMap();
        CreateMap<EmployeeForUpdateDto, Employee>().ReverseMap();
        CreateMap<CompanyForUpdateDto, Company>().ReverseMap();
    }
}
