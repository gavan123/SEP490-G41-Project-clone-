using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class MappointDAO
    {
        private readonly finsContext _context;


        public MappointDAO(finsContext context)
        {
            _context = context;
        }

        public MappointDAO()
        {
            _context = new finsContext();
        }

        // Thêm mới mappoint
        public void AddMappoint(Mappoint mappoint)
        {
            if (mappoint == null)
                throw new ArgumentNullException(nameof(mappoint));

            if (mappoint.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            if (mappoint.LocationWeb == null)
                throw new ArgumentException("LocationWeb cannot be null.");

            if (mappoint.LocationApp == null)
                throw new ArgumentException("LocationWeb cannot be null.");

            _context.Mappoints.Add(mappoint);
            _context.SaveChanges();
        }

        // Đọc thông tin mappoint bằng Id
        public Mappoint GetMappointById(int mappointId)
        {
            if (mappointId <= 0)
                throw new ArgumentException("Mappoint ID must be a positive integer.");

            return _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mappointId);
        }

        // Cập nhật thông tin mappoint
        public void UpdateMappoint(Mappoint mappoint)
        {
            if (mappoint == null)
                throw new ArgumentNullException(nameof(mappoint));

            if (mappoint.MapPointId <= 0)
                throw new ArgumentException("Mappoint ID must be a positive integer.");

            if (mappoint.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            if (mappoint.LocationWeb == null)
                throw new ArgumentException("LocationWeb cannot be null.");

            if (mappoint.LocationApp == null)
                throw new ArgumentException("LocationWeb cannot be null.");

            var existingMappoint = _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mappoint.MapPointId);
            if (existingMappoint != null)
            {
                existingMappoint.MapId = mappoint.MapId;
                existingMappoint.MapPointName = mappoint.MapPointName;
                existingMappoint.LocationWeb = mappoint.LocationWeb;
                existingMappoint.LocationApp = mappoint.LocationApp;
                existingMappoint.LocationGps = mappoint.LocationGps;
                existingMappoint.FloorId = mappoint.FloorId;
                existingMappoint.BuildingId = mappoint.BuildingId;
                existingMappoint.Image = mappoint.Image;
                existingMappoint.Destination = mappoint.Destination;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Mappoint with ID {mappoint.MapPointId} does not exist.");
            }
        }

        // Xóa mappoint bằng Id
        public void DeleteMappoint(int mappointId)
        {
            if (mappointId <= 0)
                throw new ArgumentException("Mappoint ID must be a positive integer.");

            var mappoint = _context.Mappoints
                .Include(m => m.Mappointices) // Include related Mappointex entries
                .Include(m => m.Mappointroutes) // Include related Mappointroute entries
                .SingleOrDefault(m => m.MapPointId == mappointId);

            if (mappoint != null)
            {
                // Remove edges associated with the map point
                var edgesToRemove = _context.Edges.Where(e => e.MapPointA == mappointId || e.MapPointB == mappointId);
                _context.Edges.RemoveRange(edgesToRemove);

                // Remove related Mappointex entries
                _context.Mappointices.RemoveRange(mappoint.Mappointices);

                // Remove related Mappointroute entries
                _context.Mappointroutes.RemoveRange(mappoint.Mappointroutes);

                // Remove the map point itself
                _context.Mappoints.Remove(mappoint);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Mappoint with ID {mappointId} does not exist.");
            }
        }


        // Lấy tất cả các mappoint
        public List<Mappoint> GetAllMappoints()
        {
            try
            {
                return _context.Mappoints.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }

        // Tìm kiếm mappoint bằng tên
        public Mappoint SearchByName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or empty.");

            try
            {
                // Sử dụng LINQ để tìm kiếm mappoint theo tên
                return _context.Mappoints.FirstOrDefault(mp => mp.MapPointName.ToLower() == value.ToLower());
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while searching for map point by name.", ex);
            }
        }

    }
}
