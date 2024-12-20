﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public partial class Term : BaseEntity
    {
        public string? Name { get; set; }
        public int StartWeek { get; set; }
        public int EndWeek { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SchoolYearId { get; set; }

        public ICollection<SchoolSchedule> SchoolSchedules { get; set; } = new List<SchoolSchedule>();
        public SchoolYear? SchoolYear { get; set; }
        public ICollection<CurriculumDetail> CurriculumDetails { get; set; } = new List<CurriculumDetail>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
        public ICollection<RoomSubject> RoomSubjects { get; set; } = new List<RoomSubject>();
    }
}
