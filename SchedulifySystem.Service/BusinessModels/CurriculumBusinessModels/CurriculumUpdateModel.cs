﻿using SchedulifySystem.Service.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.CurriculumBusinessModels
{
    public class CurriculumUpdateModel
    {
        public string? CurriculumName { get; set; }
        public EGrade Grade { get; set; }
        public List<int> ElectiveSubjectIds { get; set; } = new List<int>();
        public List<int> SpecializedSubjectIds { get; set; } = new List<int>();
        [JsonIgnore]
        public DateTime? UpdateDate { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public int? SchoolId { get; set; }
    }
}