﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulifySystem.Repository.EntityModels
{
    public partial class TeacherGroup : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int SchoolId { get; set; }

        public School? School { get; set; }
        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}