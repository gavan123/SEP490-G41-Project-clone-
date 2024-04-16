using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Mappointroute
    {
        public int MprId { get; set; }
        public int MapPointId { get; set; }
        public int RouteId { get; set; }

<<<<<<< HEAD
        public virtual Mappoint MapPoint { get; set; }
        public virtual Route Route { get; set; }
=======
        public virtual Mappoint MapPoint { get; set; } = null!;
        public virtual Route Route { get; set; } = null!;
>>>>>>> main
    }
}
