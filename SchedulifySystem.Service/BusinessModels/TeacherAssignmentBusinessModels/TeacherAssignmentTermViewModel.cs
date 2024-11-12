﻿using SchedulifySystem.Repository.EntityModels;
using SchedulifySystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.TeacherAssignmentBusinessModels
{
    public class TeacherAssignmentTermViewModel : BaseEntity
    {
        public int PeriodCount { get; set; }
        public int StudentClassId { get; set; }
        public AssignmentType AssignmentType { get; set; } = AssignmentType.Permanent;
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public int? TeacherId { get; set; }
        public string? TeacherFirstName { get;set; }
        public string? TeacherLastName { get; set; }
        public string? TeacherAbbreviation { get; set; }
        public int TermId { get; set; }
        public string? TermName { get; set;}
    }
}