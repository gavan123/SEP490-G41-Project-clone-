﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Route
    {
        public Route()
        {
            Mappointroutes = new HashSet<Mappointroute>();
        }

        public int RouteId { get; set; }
<<<<<<< HEAD
        public string RouteName { get; set; }
=======
        public string RouteName { get; set; } = null!;
>>>>>>> main
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public double? Distance { get; set; }

        public virtual ICollection<Mappointroute> Mappointroutes { get; set; }
    }
}
