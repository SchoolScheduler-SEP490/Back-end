﻿using SchedulifySystem.Service.BusinessModels.StudentClassBusinessModels;
using SchedulifySystem.Service.ViewModels.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.Services.Interfaces
{
    public interface IStudentClassService
    {
        Task<BaseResponseModel> GetStudentClasses(int schoolId, int? SchoolYearId,bool includeDeleted, int pageIndex, int pageSize);
        Task<BaseResponseModel> GetStudentClassById(int id);
        Task<BaseResponseModel> CreateStudentClass(CreateStudentClassModel createStudentClassModel);
        Task<BaseResponseModel> CreateStudentClasses(List<CreateStudentClassModel> createStudentClassModels);
        //Task<BaseResponseModel> UpdateStudentClass(int id, UpdateTeacherModel updateTeacherRequestModel);
        //Task<BaseResponseModel> DeleteStudentClass(int id);
    }
}
