using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Building
    {
        public Building()
        {
            Floors = new HashSet<Floor>();
            Mappoints = new HashSet<Mappoint>();
        }

        public int BuildingId { get; set; }
        public string BuildingName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int FacilityId { get; set; }

        public virtual Facility Facility { get; set; } = null!;
        public virtual ICollection<Floor> Floors { get; set; }
        public virtual ICollection<Mappoint> Mappoints { get; set; }
    }
}
