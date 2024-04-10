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
        private readonly finsContext _context; private readonly WKBReader _wkbReader;

        public MappointDAO(finsContext context)
        {
            _context = context;
            _wkbReader = new WKBReader();
        }

        // Thêm mới mappoint
        public void AddMappoint(Mappoint mappoint)
        {
            _context.Mappoints.Add(mappoint);
            _context.SaveChanges();
        }

        // Đọc thông tin mappoint bằng Id
        public Mappoint GetMappointById(int mappointId)
        {
            return _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mappointId);
        }

        // Cập nhật thông tin mappoint
        public void UpdateMappoint(Mappoint mappoint)
        {
            var existingMappoint = _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mappoint.MapPointId);

            if (existingMappoint != null)
            {
                existingMappoint.MapId = mappoint.MapId;
                existingMappoint.Location = mappoint.Location;
                // Cập nhật các thuộc tính khác nếu cần

                _context.SaveChanges();
            }
        }

        // Xóa mappoint bằng Id
        public void DeleteMappoint(int mappointId)
        {
            var mappoint = _context.Mappoints.FirstOrDefault(mp => mp.MapPointId == mappointId);
            if (mappoint != null)
            {
                _context.Mappoints.Remove(mappoint);
                _context.SaveChanges();
            }
        }

        public List<Mappoint> GetAllMappoints()
        {
            try
            {
                var mappoints = _context.Mappoints.Include(mp => mp.Map).ToList();


                /*// Đọc dữ liệu địa lý từ MySQL dưới dạng chuỗi và chuyển đổi thành kiểu NetTopologySuite.Point
                foreach (var mappoint in mappoints)
                {
                    // Đọc dữ liệu POINT từ MySQL dưới dạng chuỗi
                    string locationString = mappoint.Location.ToString();
                    Console.WriteLine("LocationString: " + locationString);

                    // Chuyển đổi chuỗi thành đối tượng Point của NetTopologySuite
                    var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
                    var wktReader = new WKTReader(geometryFactory);
                    var point = (Point)wktReader.Read(locationString);

                    // Gán đối tượng Point cho thuộc tính Location của đối tượng Mappoint
                    mappoint.Location = point;
                }*/

                return mappoints;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }


    }

}

