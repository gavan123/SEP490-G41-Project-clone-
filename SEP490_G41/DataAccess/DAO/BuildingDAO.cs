using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class BuildingDAO
    {
        private readonly finsContext _context;
       
        public BuildingDAO(finsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public BuildingDAO()
        {
            _context = new finsContext(); // Initialize with a new instance for testing
        }
        
        // Thêm mới tòa nhà
        public void AddBuilding(Building building)
        {
            if (building == null)
            {
                throw new ArgumentNullException(nameof(building));
            }

            _context.Buildings.Add(building);
            _context.SaveChanges();
            _context.Dispose();
        }

        // Đọc thông tin tòa nhà bằng Id
        public Building GetBuildingById(int buildingId)
        {
            if (buildingId <= 0)
            {
                throw new ArgumentException("Invalid building ID", nameof(buildingId));
            }
            var building = _context.Buildings.FirstOrDefault(b => b.BuildingId == buildingId);
            _context.Dispose();
            return building;
        }

        // Cập nhật thông tin tòa nhà
        public void UpdateBuilding(Building building)
        {
            if (building == null)
            {
                throw new ArgumentNullException(nameof(building));
            }

            var existingBuilding = _context.Buildings.FirstOrDefault(b => b.BuildingId == building.BuildingId);

            if (existingBuilding != null)
            {
                existingBuilding.BuildingName = building.BuildingName;
                existingBuilding.Image = building.Image;
                existingBuilding.Status = building.Status;
                existingBuilding.FacilityId = building.FacilityId;

                _context.SaveChanges();
                _context.Dispose();
            }
            else
            {
                throw new ArgumentException("Building not found", nameof(building));
            }
        }

        // Xóa tòa nhà bằng Id
        public void DeleteBuilding(int buildingId)
        {
            if (buildingId <= 0)
            {
                throw new ArgumentException("Invalid building ID", nameof(buildingId));
            }

            try
            {
                var floors = _context.Floors.Where(f => f.BuildingId == buildingId).ToList();

                // Xóa từng tầng trong danh sách
                foreach (var floor in floors)
                {
                    var maps = _context.Maps.Where(m => m.FloorId == floor.FloorId).ToList();
                    foreach (var map in maps)
                    {
                        var mapId = map.MapId;
                        // Xóa các dữ liệu liên quan đến bản đồ
                        var mappoints = _context.Mappoints.Where(mp => mp.MapId == mapId).ToList();
                        foreach (var mappoint in mappoints)
                        {
                            // Xóa các dữ liệu liên quan đến Mappoint
                            var mappointId = mappoint.MapPointId;
                            var mappointexs = _context.Mappointices.Where(me => me.MapPointId == mappointId).ToList();
                            _context.Mappointices.RemoveRange(mappointexs);
                            var mappointroutes = _context.Mappointroutes.Where(mr => mr.MapPointId == mappointId).ToList();
                            _context.Mappointroutes.RemoveRange(mappointroutes);
                            var edges = _context.Edges.Where(e => e.MapPointA == mappointId || e.MapPointB == mappointId).ToList();
                            _context.Edges.RemoveRange(edges);

                            _context.Mappoints.Remove(mappoint);
                        }

                        _context.Maps.Remove(map);
                    }

                    _context.Floors.Remove(floor);
                }

                // Lấy tòa nhà cần xóa
                var building = _context.Buildings.FirstOrDefault(b => b.BuildingId == buildingId);
                if (building != null)
                {
                    _context.Buildings.Remove(building);
                    _context.SaveChanges();
                    _context.Dispose();
                }
                else
                {
                    throw new ArgumentException("Building not found", nameof(buildingId));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting building: {ex.Message}");
                throw;
            }
        }


        // Lấy danh sách tất cả các tòa nhà
        public List<Building> GetAllBuildings()
        {
            var buildings =  _context.Buildings
                   .Include(b => b.Facility)
                   .ToList();
            _context.Dispose();
            return buildings;
        }

    }
}
