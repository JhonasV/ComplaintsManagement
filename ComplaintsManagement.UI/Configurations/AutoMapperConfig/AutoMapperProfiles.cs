using AutoMapper;
using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.Infrastructure.Entities;
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
        }
    }
}