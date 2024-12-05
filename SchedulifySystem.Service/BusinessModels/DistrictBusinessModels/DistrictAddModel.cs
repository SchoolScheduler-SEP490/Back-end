﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SchedulifySystem.Service.BusinessModels.DistrictBusinessModels
{
    public class DistrictAddModel
    {
        public string? Name { get; set; }
        [JsonIgnore]
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    }
}