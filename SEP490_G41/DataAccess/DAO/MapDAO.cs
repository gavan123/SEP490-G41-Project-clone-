using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.DAO
{
    public class MapDAO
    {
        private readonly finsContext _context;

        public MapDAO(finsContext context)
        {
            _context = context;
        }

        // Thêm mới bản đồ
        public void AddMap(Map map)
        {
            _context.Maps.Add(map);
            _context.SaveChanges();
        }

        // Đọc thông tin bản đồ bằng Id
        public Map GetMapById(int mapId)
        {
            return _context.Maps.Include(m => m.Floor).FirstOrDefault(m => m.MapId == mapId);
        }

        // Cập nhật thông tin bản đồ
        public void UpdateMap(Map map)
        {
            var existingMap = _context.Maps.FirstOrDefault(m => m.MapId == map.MapId);

            if (existingMap != null)
            {
                existingMap.MapName = map.MapName;
                existingMap.Image2D = map.Image2D;
                existingMap.Image3D = map.Image3D;
                existingMap.FloorId = map.FloorId;

                _context.SaveChanges();
            }
        }

        // Xóa bản đồ bằng Id
        public void DeleteMap(int mapId)
        {
            var map = _context.Maps.FirstOrDefault(m => m.MapId == mapId);
            if (map != null)
            {
                _context.Maps.Remove(map);
                _context.SaveChanges();
            }
        }

        // Lấy danh sách tất cả các bản đồ
        public List<Map> GetAllMaps()
        {
            return _context.Maps.Include(m => m.Floor).ToList();
        }

        // Tìm kiếm bản đồ theo tên
        public List<Map> SearchMapsByName(string keyword)
        {
            var maps = _context.Maps
                .Where(m => m.MapName.Contains(keyword))
                .ToList();

            return maps;
        }
    }
}
