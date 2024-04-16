﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Map
    {
        public Map()
        {
            Mapmanages = new HashSet<Mapmanage>();
            Mappoints = new HashSet<Mappoint>();
        }

        public int MapId { get; set; }
        public string MapName { get; set; }
        public string Image2D { get; set; }
        public string Image3D { get; set; }
        public int FloorId { get; set; }

        public virtual Floor Floor { get; set; }
        public virtual ICollection<Mapmanage> Mapmanages { get; set; }
        public virtual ICollection<Mappoint> Mappoints { get; set; }
    }
}
