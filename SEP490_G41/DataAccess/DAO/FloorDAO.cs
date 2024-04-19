using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class FloorDAO
    {
        private readonly finsContext _context;

        public FloorDAO(finsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Thêm mới tầng
        public void AddFloor(Floor floor)
        {
            if (floor == null)
            {
                throw new ArgumentNullException(nameof(floor));
            }

            _context.Floors.Add(floor);
            _context.SaveChanges();
        }
        // Đọc thông tin tầng bằng Id
        public Floor GetFloorById(int floorId)
        {
            if (floorId <= 0)
            {
                throw new ArgumentException("Invalid floor ID", nameof(floorId));
            }

            return _context.Floors.FirstOrDefault(f => f.FloorId == floorId);
        }

        public List<Floor> GetAllFloorsByBuildingId(int buildingId)
        {
            if (buildingId <= 0)
            {
                throw new ArgumentException("Invalid building ID", nameof(buildingId));
            }

            return _context.Floors.Where(f => f.BuildingId == buildingId).ToList();
        }


        public void UpdateFloor(Floor floor)
        {
            if (floor == null)
            {
                throw new ArgumentNullException(nameof(floor));
            }

            var existingFloor = _context.Floors.FirstOrDefault(f => f.FloorId == floor.FloorId);

            if (existingFloor != null)
            {
                existingFloor.FloorName = floor.FloorName;
                existingFloor.Greeting = floor.Greeting;
                existingFloor.Status = floor.Status;
                existingFloor.BuildingId = floor.BuildingId;

                _context.SaveChanges();
            }
        }

        public void DeleteFloor(int floorId)
        {
            if (floorId <= 0)
            {
                throw new ArgumentException("Invalid floor ID", nameof(floorId));
            }

            var floor = _context.Floors.FirstOrDefault(f => f.FloorId == floorId);
            if (floor != null)
            {
                _context.Floors.Remove(floor);
                _context.SaveChanges();
            }
        }
        public List<Floor> GetAllFloors()
        {
            return _context.Floors.ToList();
        }

    }
}
