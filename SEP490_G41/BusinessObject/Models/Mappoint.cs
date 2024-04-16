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
            Mappointroutes = new HashSet<Mappointroute>();
        }

        public int MapPointId { get; set; }
        public int MapId { get; set; }
<<<<<<< HEAD
        public string MappointName { get; set; }
        public Point LocationWeb { get; set; }
        public Point LocationApp { get; set; }
        public Point LocationGps { get; set; }
        public int? FloorId { get; set; }
        public int? BuildingId { get; set; }
        public string Image { get; set; }

        public virtual Map Map { get; set; }
=======
        public string? MappointName { get; set; }
        public int? FloorId { get; set; }
        public int? BuildingId { get; set; }
        public string? Image { get; set; }

        public virtual Map Map { get; set; } = null!;
>>>>>>> main
        public virtual ICollection<Mappointroute> Mappointroutes { get; set; }
    }
}
