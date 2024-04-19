using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace BusinessObject.Models
{
    public partial class Section
    {
        public int SectionId { get; set; }
        public int FloorId { get; set; }
        public string SectionName { get; set; }
        public int Long { get; set; }
        public int Width { get; set; }
        public Point UpCorner { get; set; }
        public Point DownCorner { get; set; }

        public virtual Floor Floor { get; set; }
    }
}
