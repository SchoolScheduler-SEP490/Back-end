﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchedulifySystem.Service.Services.Interfaces;

namespace SchedulifySystem.API.Controllers
{
    [Route("api/student-classes")]
    [ApiController]
    public class StudentClassController : BaseController
    {
        private readonly IStudentClassService _studentClassService;

        public StudentClassController(IStudentClassService studentClassService)
        {
            _studentClassService = studentClassService;
        }

        [HttpGet]
        //[Authorize]
        public Task<IActionResult> GetStudentClasses(int schoolId, int? schoolYearId = null, bool includeDeleted = false, int pageIndex = 1, int pageSize = 20)
        {
            return ValidateAndExecute(() => _studentClassService.GetStudentClasses(schoolId, schoolYearId, includeDeleted, pageIndex, pageSize));
        }


        [HttpGet("{id}")]
        [Authorize]
        public Task<IActionResult> GetStudentClassById(int id)
        {
            return ValidateAndExecute(() => _studentClassService.GetStudentClassById(id));
        }
    }
}
