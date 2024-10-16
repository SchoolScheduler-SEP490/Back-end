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
        Task<BaseResponseModel> GetStudentClasses(int schoolId,int? gradeId, int? SchoolYearId,bool includeDeleted, int pageIndex, int pageSize);
        Task<BaseResponseModel> GetStudentClassById(int id);
        Task<BaseResponseModel> CreateStudentClass(CreateStudentClassModel createStudentClassModel);
        Task<BaseResponseModel> DeleteStudentClass(int id);
        Task<BaseResponseModel> CreateStudentClasses(int schoolId, int schoolYearId, List<CreateListStudentClassModel> createStudentClassModels);
        Task<BaseResponseModel> UpdateStudentClass(int id, UpdateStudentClassModel updateStudentClassModel);
        Task<BaseResponseModel> AssignHomeroomTeacherToClasses(AssignListStudentClassModel assignListStudentClassModel);

    }
}
