using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace BusinessObject.Models
{
    public partial class Mappoint
    {
        public Mappoint()
        {
            EdgeMapPointANavigations = new HashSet<Edge>();
            EdgeMapPointBNavigations = new HashSet<Edge>();
            Mappointices = new HashSet<Mappointex>();
            Mappointroutes = new HashSet<Mappointroute>();
        }

        public int MapPointId { get; set; }
        public string MapPointName { get; set; }
        public string Image { get; set; }
        public Point LocationWeb { get; set; }
        public Point LocationApp { get; set; }
        public Point LocationGps { get; set; }
        public int MapId { get; set; }
        public int BuildingId { get; set; }
        public int FloorId { get; set; }
        public bool? Destination { get; set; }

        public virtual Building Building { get; set; }
        public virtual Floor Floor { get; set; }
        public virtual Map Map { get; set; }
        public virtual ICollection<Edge> EdgeMapPointANavigations { get; set; }
        public virtual ICollection<Edge> EdgeMapPointBNavigations { get; set; }
        public virtual ICollection<Mappointex> Mappointices { get; set; }
        public virtual ICollection<Mappointroute> Mappointroutes { get; set; }
    }
}
