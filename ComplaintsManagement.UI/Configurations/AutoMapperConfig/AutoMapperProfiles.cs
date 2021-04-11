using AutoMapper;
using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
using ComplaintsManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComplaintsManagement.UI.Configurations.AutoMapperConfig
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TicketTypes, TicketTypesDto>().ReverseMap();
            CreateMap<Complaints, ComplaintsDto>().ReverseMap();
            CreateMap<ComplaintsOptions, ComplaintsOptionsDto>().ReverseMap();
            CreateMap<Status, StatusDto>().ReverseMap();
            CreateMap<Products, ProductsDto>().ReverseMap();
            CreateMap<Departments, DepartmentsDto>().ReverseMap();
            CreateMap<ApplicationUser, UsersDto>().ReverseMap();
            CreateMap<Binnacle, BinnacleDto>().ReverseMap();
        }
    }
}