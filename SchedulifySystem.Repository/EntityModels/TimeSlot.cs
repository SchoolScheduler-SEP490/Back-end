﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public class TimeSlot
    {
        public int TimeSlotId { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int SchoolId { get; set; }

        public School School { get; set; }
        public ICollection<ClassPeriod> ClassPeriods { get; set; }
    }
}
