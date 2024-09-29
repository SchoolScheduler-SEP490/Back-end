﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.ViewModels.ResponseModels.TeacherResponseModels
{
    public class TeacherResponseModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Abbreviation { get; set; }
        public string? Email { get; set; }
        public int Gender { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int SchoolId { get; set; }
        public int TeacherGroupId { get; set; }
        public int TeacherRole { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
