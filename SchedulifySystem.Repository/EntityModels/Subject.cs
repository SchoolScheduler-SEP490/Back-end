﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public partial class Subject : BaseEntity
    {
        public string? SubjectName { get; set; }
        public string? Abbreviation { get; set; }
        public bool IsRequired { get; set; }
        public string? Description { get; set; }
        public int? TotalSlotInYear { get; set; }
        public int? SlotSpecialized {  get; set; }
        public int? SubjectGroupType { get; set; }
        public int SchoolYearId { get; set; }
        public bool IsTeachedByHomeroomTeacher { get; set; } = false;

        public SchoolYear? SchoolYear { get; set; }
        public ICollection<CurriculumDetail> CurriculumDetails { get; set; } = new List<CurriculumDetail>();
        public ICollection<TeachableSubject> TeachableSubjects { get; set; } = new List<TeachableSubject>();
        public ICollection<ClassPeriod> ClassPeriods { get; set; } = new List<ClassPeriod>();
        public ICollection<RoomSubject> RoomSubjects { get; set; } = new List<RoomSubject>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}
