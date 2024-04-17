using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class MapDTO
    {
        public int MapId { get; set; }
        public string MapName { get; set; } = null!;
        public string Image2D { get; set; } = null!;
        public string? Image3D { get; set; }
        public int FloorId { get; set; }
        public string FloorName { get; set; }
        public string BuildingName { get; set; }
        public string ManagerFullName { get; set; }
        public string BuildingImg { get; set; }
    }

    public class MapAddDTO
    {
        public string MapName { get; set; } = null!;
        public string Image2D { get; set; } = null!;
        public string? Image3D { get; set; }
        public int FloorId { get; set; }
    }

    public class MapUpdateDTO
    {
        public int MapId { get; set; }
        public string MapName { get; set; } = null!;
        public string Image2D { get; set; } = null!;
        public string? Image3D { get; set; }
        public int FloorId { get; set; }
    }
}
