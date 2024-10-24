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
        public int? SchoolId { get; set; }
        public int? TotalSlotInYear { get; set; }
        public int? SlotSpecialized {  get; set; }
        public int? SubjectGroupType { get; set; }
        public School? School { get; set; }  

        public ICollection<SubjectInGroup> SubjectInGroups { get; set; } = new List<SubjectInGroup>();
        public ICollection<SchoolSchedule> SchoolSchedules { get; set; } = new List<SchoolSchedule>();
        public ICollection<TeachableSubject> TeachableSubjects { get; set; } = new List<TeachableSubject>();
        public ICollection<SubjectConfig> SubjectConfigs { get; set; } = new List<SubjectConfig>();
        public ICollection<ClassPeriod> ClassPeriods { get; set; } = new List<ClassPeriod>();
        public ICollection<RoomSubject> RoomSubjects { get; set; } = new List<RoomSubject>();
        public ICollection<TeacherAssignment> TeacherAssignments { get; set; } = new List<TeacherAssignment>();
    }
}
