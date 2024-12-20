﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.ScheduleBusinessMoldes
{
    public class SchoolScheduleViewModel
    {
        public int SchoolYearId { get; set; }
        public string? StartYear { get; set; }
        public string? EndYear { get; set; }
        public int SchoolId { get; set; }
        public int TermId { get; set; }
        public string TermName { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public int WeeklyRange { get; set; }
        public string? Name { get; set; }
        public int FitnessPoint { get; set; }
    }
}
