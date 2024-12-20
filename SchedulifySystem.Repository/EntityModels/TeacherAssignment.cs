﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public partial class TeacherAssignment : BaseEntity
    {
        public int? TeacherId { get; set; }
        public int SubjectId { get; set; }
        public int StudentClassId { get; set; }
        public int AssignmentType { get; set; }
        public int PeriodCount { get; set; } // số lượng tiết trên tuần 
        public int TermId { get; set; }
        public int? RoomSubjectId { get; set; }

        public RoomSubject? RoomSubject { get; set; }
        public Teacher? Teacher { get; set; }
        public Subject? Subject { get; set; }
        public Term? Term { get; set; }
        public StudentClass? StudentClass { get; set; }
    }
}
