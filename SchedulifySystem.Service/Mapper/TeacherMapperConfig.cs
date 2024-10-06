﻿using AutoMapper;
using SchedulifySystem.Repository.EntityModels;
using SchedulifySystem.Service.BusinessModels.TeacherBusinessModels;
using SchedulifySystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.Mapper
{
    public partial class MapperConfigs : Profile
    {
        partial void TeacherMapperConfig()
        {
            CreateMap<Teacher, TeacherViewModel>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
                .ForMember(dest => dest.TeacherGroupName, opt => opt.MapFrom(src => src.Group != null ? src.Group.Name : string.Empty))
                .ForMember(dest => dest.TeachableSubjects, opt => opt.MapFrom(src => src.TeachableSubjects.Select(ts => ts.Subject != null ? ts.Subject.SubjectName : string.Empty)))
                .ForMember(dest => dest.Gender, otp => otp.MapFrom(src => src.Gender == (int)Gender.Female ? "Nữ" : "Nam"));

            CreateMap<CreateTeacherModel, Teacher>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<CreateListTeacherModel, Teacher>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.UtcNow));

            CreateMap<UpdateTeacherModel, Teacher>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth)))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}
