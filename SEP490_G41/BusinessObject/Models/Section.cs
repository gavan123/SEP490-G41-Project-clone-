using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Section
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; } = null!;
        public int Width { get; set; }
        public int Length { get; set; }
        public int FloorId { get; set; }

        public virtual Floor Floor { get; set; } = null!;
    }
}
