using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Floor
    {
        public Floor()
        {
            Maps = new HashSet<Map>();
        }

        public int FloorId { get; set; }
        public string FloorName { get; set; } = null!;
        public string Greeting { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; } = null!;
        public virtual ICollection<Map> Maps { get; set; }
    }
}
