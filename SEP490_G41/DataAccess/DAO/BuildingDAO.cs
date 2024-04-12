﻿using BusinessObject.Models;
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
            _context = context;
        }

        // Thêm mới tòa nhà
        public void AddBuilding(Building building)
        {
            _context.Buildings.Add(building);
            _context.SaveChanges();
        }

        // Đọc thông tin tòa nhà bằng Id
        public Building GetBuildingById(int buildingId)
        {
            return _context.Buildings.FirstOrDefault(b => b.BuildingId == buildingId);
        }

        // Cập nhật thông tin tòa nhà
        public void UpdateBuilding(Building building)
        {
            var existingBuilding = _context.Buildings.FirstOrDefault(b => b.BuildingId == building.BuildingId);

            if (existingBuilding != null)
            {
                existingBuilding.BuildingName = building.BuildingName;
                existingBuilding.Image = building.Image;
                existingBuilding.Status = building.Status;
                existingBuilding.FacilityId = building.FacilityId;

                _context.SaveChanges();
            }
        }

        // Xóa tòa nhà bằng Id
        public void DeleteBuilding(int buildingId)
        {
            try
            {
                var floors = _context.Floors.Where(f => f.BuildingId == buildingId).ToList();

                // Xóa từng tầng trong danh sách
                foreach (var floor in floors)
                {
                    _context.Floors.Remove(floor);
                }

                // Lấy tòa nhà cần xóa
                var building = _context.Buildings.FirstOrDefault(b => b.BuildingId == buildingId);
                if (building != null)
                {
                    _context.Buildings.Remove(building);
                    _context.SaveChanges(); 
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
            return _context.Buildings
                   .Include(b => b.Facility)  
                   .ToList();
        }
       
    }
}
