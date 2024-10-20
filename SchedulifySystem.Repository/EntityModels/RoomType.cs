﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SchedulifySystem.Repository.EntityModels
{
    public partial class RoomType : BaseEntity
    {
        public int SchoolId { get; set; }
        public string? Name { get; set; }
        public string? RoomTypeCode { get; set; }

        public School? School { get; set; }
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
    }
}
