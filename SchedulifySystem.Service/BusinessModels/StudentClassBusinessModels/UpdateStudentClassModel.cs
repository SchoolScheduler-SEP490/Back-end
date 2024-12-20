﻿using SchedulifySystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.StudentClassBusinessModels
{
    public class UpdateStudentClassModel
    {
        public string? Name { get; set; }
        public int? HomeroomTeacherId { get; set; }
        public int? SchoolYearId { get; set; }
        public MainSession? MainSession { get; set; }
        public bool IsFullDay { get; set; }
        public EGrade Grade { get; set; }
        public int? RoomId { get; set; }
    }
}
