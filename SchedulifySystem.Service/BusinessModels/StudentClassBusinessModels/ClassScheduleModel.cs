﻿using SchedulifySystem.Repository.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.StudentClassBusinessModels
{
    public class ClassScheduleModel : BaseEntity
    {
        public string? Name { get; set; }
        public int? HomeroomTeacherId { get; set; }
        public int SchoolId { get; set; }
        public int SchoolYearId { get; set; }
        public int MainSession { get; set; }
        public bool IsFullDay { get; set; }
        public int PeriodCount { get; set; }
        public int StudentClassGroupId { get; set; }
        public int CurriculumId { get; set; }
        public string? RoomCode {  get; set; }
        public int? RoomId { get; set; } = 0;

        public ClassScheduleModel(StudentClass studentClass)
        {
            Id = studentClass.Id;
            Name = studentClass.Name;
            HomeroomTeacherId = studentClass.HomeroomTeacherId;
            SchoolId = studentClass.SchoolId;
            SchoolYearId = studentClass.SchoolYearId;
            MainSession = studentClass.MainSession;
            IsFullDay = studentClass.IsFullDay;
            PeriodCount = studentClass.PeriodCount;
            StudentClassGroupId = (int) studentClass.StudentClassGroupId;
            CurriculumId = (int)studentClass.StudentClassGroup.CurriculumId;
            RoomCode = studentClass.Room.RoomCode;
            RoomId = studentClass.RoomId;
        }
    }
}
