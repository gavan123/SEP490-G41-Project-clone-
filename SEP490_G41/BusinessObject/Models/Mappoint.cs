using System;
using System.Collections.Generic;
using System.Drawing;
using NetTopologySuite.Geometries;
using NetTopologySuite.Geometries.Utilities;


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
        public int MapId { get; set; }
        public NetTopologySuite.Geometries.Point Location { get; set; }
        public virtual Map Map { get; set; } = null!;
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<Route> RouteEndPointNavigations { get; set; }
        public virtual ICollection<Route> RouteStartPointNavigations { get; set; }
    }
}
