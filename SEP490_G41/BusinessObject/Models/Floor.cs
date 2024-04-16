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
        public string FloorName { get; set; }
        public string Greeting { get; set; }
        public string Status { get; set; }
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; }
        public virtual ICollection<Map> Maps { get; set; }
    }
}
