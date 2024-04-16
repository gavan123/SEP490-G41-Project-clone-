using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;


namespace BusinessObject.Models
{
    public partial class Mappoint
    {
        public Mappoint()
        {
            Mappointroutes = new HashSet<Mappointroute>();
        }

        public int MapPointId { get; set; }
        public int MapId { get; set; }
        public Point LocationWeb { get; set; }
        public Point LocationApp { get; set; }
        public Point LocationGps { get; set; }
        public string? MappointName { get; set; }
        public int? FloorId { get; set; }
        public int? BuildingId { get; set; }
        public string? Image { get; set; }

        public virtual Map Map { get; set; } = null!;

        public virtual ICollection<Mappointroute> Mappointroutes { get; set; }
    }
}
