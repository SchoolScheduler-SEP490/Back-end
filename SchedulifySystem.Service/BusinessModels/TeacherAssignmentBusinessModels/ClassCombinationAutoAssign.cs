﻿using SchedulifySystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.TeacherAssignmentBusinessModels
{
    public class ClassCombinationAutoAssign
    {
        public int SubjectId { get; set; }
        public List<int> ClassIds { get; set; }
        public int RoomId { get; set; }
        public MainSession Session { get; set; }
        public int? TeacherId { get; set; }
        [JsonIgnore]
        public int Id { get; set; }
    }
}
