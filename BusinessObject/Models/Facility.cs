using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Facility
    {
        public Facility()
        {
            Buildings = new HashSet<Building>();
        }

        public int FacilityId { get; set; }
        public string FacilityName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual ICollection<Building> Buildings { get; set; }
    }
}
