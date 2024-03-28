using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class FacilityDTO
    {
        public int FacilityId { get; set; }
        public string FacilityName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

    public class FacilityAddDTO
    { 
        public string BuildingName { get; set; } = null!;
        public string FacilityName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Status { get; set; } = null!;
    }

    public class FacilityUpdateDTO
    {
        public string FacilityName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
