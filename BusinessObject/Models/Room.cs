using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Room
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int FloorId { get; set; }
        public int MapPointId { get; set; }

        public virtual Floor Floor { get; set; } = null!;
        public virtual Mappoint MapPoint { get; set; } = null!;
    }
}
