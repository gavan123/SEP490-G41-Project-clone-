using AutoMapper.Configuration.Conventions;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
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

            if (mappoint.Location == null)
                throw new ArgumentException("Location cannot be null.");

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

            if (mappoint.Location == null)
                throw new ArgumentException("Location cannot be null.");

            var existingMappoint = _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mappoint.MapPointId);
            if (existingMappoint != null)
            {
                existingMappoint.MapId = mappoint.MapId;
                existingMappoint.Location = mappoint.Location;
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

            var mappoint = _context.Mappoints.Find(mappointId);
            if (mappoint != null)
            {
                _context.Mappoints.Remove(mappoint);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Mappoint with ID {mappointId} does not exist.");
            }
        }


        public List<Mappoint> GetAllMappoints()
        {
            try
            {
                return _context.Mappoints.Include(mp => mp.Map).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }
    }
}