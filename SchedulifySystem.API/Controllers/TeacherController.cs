﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchedulifySystem.Service.Services.Interfaces;
using SchedulifySystem.Service.BusinessModels.TeacherBusinessModels;
using System.ComponentModel.DataAnnotations;
using SchedulifySystem.Repository.EntityModels;
using SchedulifySystem.Service.Enums;

namespace SchedulifySystem.API.Controllers
{
    [Route("api/schools/{schoolId}/teachers")]
    [ApiController]
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        [Authorize(Roles = "SchoolManager, TeacherDepartmentHead, Teacher")]
        public Task<IActionResult> GetTeachers(int schoolId, string? Name, TeacherStatus? teacherStatus, int? departmentId = null, bool includeDeleted = false, int pageSize = 20, int pageIndex = 1)
        {
            return ValidateAndExecute(() => _teacherService.GetTeachers(schoolId, Name, teacherStatus, departmentId, includeDeleted, pageIndex, pageSize));
        }


        [HttpGet("{teacherId}/assignments")]
        [Authorize(Roles = "SchoolManager, TeacherDepartmentHead, Teacher")]
        public Task<IActionResult> GetTeacherAssignmentDetail([Required] int teacherId, [Required] int schoolYearId)
        {
            return ValidateAndExecute(() => _teacherService.GetTeacherAssignmentDetail(teacherId, schoolYearId));
        }

        [HttpPost]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> CreateTeachers(int schoolId, List<CreateListTeacherModel> models)
        {
            return ValidateAndExecute(() => _teacherService.CreateTeachers(schoolId, models));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> UpdateTeacher(int id, UpdateTeacherModel model)
        {
            return ValidateAndExecute(() => _teacherService.UpdateTeacher(id, model));
        }
        
        [HttpPost("{id}/teachable-subjects")]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> AddTeachableSubjects(int id, List<SubjectGradeModel> teachableSubjects)
        {
            return ValidateAndExecute(() => _teacherService.AddTeachableSubjects(id, teachableSubjects));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SchoolManager, TeacherDepartmentHead, Teacher")]
        public Task<IActionResult> GetTeacherById(int id)
        {
            return ValidateAndExecute(() => _teacherService.GetTeacherById(id));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> DeleteTeacher(int id)
        {
            return ValidateAndExecute(() => _teacherService.DeleteTeacher(id));
        }

        [HttpDelete("teachable-subject/{id}")]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> DeleteTeachableSubject(int id)
        {
            return ValidateAndExecute(() => _teacherService.DeleteTeachableSubjeect(id));
        }

        [HttpPatch("assign-department-head")]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> AssignDepartmentHead(int schoolId, List<AssignTeacherDepartmentHeadModel> models)
        {
            return ValidateAndExecute(() => _teacherService.AssignTeacherDepartmentHead(schoolId, models));
        }

        [HttpPost("generate-account")]
        [Authorize(Roles = "SchoolManager")]
        public Task<IActionResult> GenerateTeacherAccount(TeacherGenerateAccount teacherGenerateAccount)
        {
            return ValidateAndExecute(() => _teacherService.GenerateTeacherAccount(teacherGenerateAccount));
        }

        [HttpGet("{email}/info")]
        [Authorize(Roles = "Teacher, SchoolManager, TeacherDepartmentHead")]
        public Task<IActionResult> GetTeacherByEmail(int schoolId, string email)
        {
            return ValidateAndExecute(() => _teacherService.GetTeacherByEmail(schoolId, email));
        }

        [HttpGet("teacher-account")]
        public Task<IActionResult> GetTeacherAccountListAsync(int schoolId, int pageIndex = 1, int pageSize = 20)
        {
            return ValidateAndExecute(() => _teacherService.GetTeacherAccountListAsync(schoolId, pageIndex, pageSize));
        }

    }
}
