using System;
using System.Collections.Generic;
<<<<<<< HEAD
using NetTopologySuite.Geometries;
=======
>>>>>>> main

namespace BusinessObject.Models
{
    public partial class Mappoint
    {
        public Mappoint()
        {
            Rooms = new HashSet<Room>();
            RouteEndPointNavigations = new HashSet<Route>();
            RouteStartPointNavigations = new HashSet<Route>();
        }

        public int MapPointId { get; set; }
        public Point Location { get; set; } = null!;
        public int MapId { get; set; }
<<<<<<< HEAD
=======
        public string? MappointName { get; set; }
>>>>>>> main

        public virtual Map Map { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Route> RouteEndPointNavigations { get; set; }
        public virtual ICollection<Route> RouteStartPointNavigations { get; set; }
    }
}
