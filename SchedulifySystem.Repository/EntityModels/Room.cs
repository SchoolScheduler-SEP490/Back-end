﻿using System;
using System.Collections.Generic;
using System.Configuration.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public partial class Room : BaseEntity
    {
        public string? Name { get; set; }
        public int RoomTypeId { get; set; }
        public int MaxClassPerTime { get; set; }
        public int BuildingId { get; set; }
        public int AvailabilityeStatus { get; set; }

        public RoomType? RoomType { get; set; }
        public Building? Building { get; set; }
        public ICollection<ClassPeriod> ClassPeriods { get; set; } = new List<ClassPeriod>();
    }
}