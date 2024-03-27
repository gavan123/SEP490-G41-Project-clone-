using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Route
    {
        public int RouteId { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Mappoint EndPointNavigation { get; set; } = null!;
        public virtual Mappoint StartPointNavigation { get; set; } = null!;
    }
}
