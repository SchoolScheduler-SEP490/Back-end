﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public partial class Teacher : BaseEntity
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int Gender { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int SchoolId { get; set; }
        public int TeacherGroupId { get; set; }
        public int Status { get; set; }

        public Department? Department { get; set; }
        public School? School { get; set; }
        public TeacherGroup? Group { get; set; }
        public ICollection<TeachableSubject> TeachableSubjects { get; set; } = new List<TeachableSubject>();
        public ICollection<StudentClass> StudentClasses { get; set; } = new List<StudentClass>();
        public ICollection<ClassPeriod> ClassPeriods { get; set; } = new List<ClassPeriod>();
        public ICollection<TeacherConfig> TeacherConfigs { get; set; } = new List<TeacherConfig>();
        public ICollection<TeacherUnavailability> TeacherUnavailabilities { get; set; } = new List<TeacherUnavailability>();
    }
}
