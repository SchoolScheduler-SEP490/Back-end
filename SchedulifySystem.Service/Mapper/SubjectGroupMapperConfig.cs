﻿using AutoMapper;
using SchedulifySystem.Repository.EntityModels;
using SchedulifySystem.Service.BusinessModels.SubjectGroupBusinessModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.Mapper
{
    public partial class MapperConfigs : Profile
    {
        partial void SubjectGroupMapperConfig()
        {
            CreateMap<SubjectGroupAddModel, SubjectGroup>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.UtcNow)).ReverseMap();
            CreateMap<SubjectGroup, SubjectGroupViewModel>()
               .ForMember(dest => dest.SchoolName,
                opt => opt.MapFrom(src => src.School != null ? src.School.Name : string.Empty))
               .ForMember(dest => dest.SubjectGroupTypeName,
                otp => otp.MapFrom(src => src.SubjectGroupType != null ? src.SubjectGroupType.Name : string.Empty))
               .ReverseMap();
        }
    }
}
