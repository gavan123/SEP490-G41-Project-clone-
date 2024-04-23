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
        public MapDAO()
        {
            _context = new finsContext(); 
        }


        // Thêm mới bản đồ
        public void AddMap(Map map, int memberID)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));

            if (string.IsNullOrWhiteSpace(map.MapName))
                throw new ArgumentException("Map name cannot be null or empty.");

            if (map.MapImage2D == null)
                throw new ArgumentException("2D image cannot be null.");

            if (map.FloorId <= 0)
                throw new ArgumentException("Floor ID must be a positive integer.");

            // Thêm mới bản đồ
            _context.Maps.Add(map);
            _context.SaveChanges();

            // Thêm mới bản đồ quản lý (Mapmanage)
            var mapManage = new Mapmanage
            {
                MapId = map.MapId,
                MemberId = memberID,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            _context.Mapmanages.Add(mapManage);

            // Lưu thay đổi
            _context.SaveChanges();
        }

        // Đọc thông tin bản đồ bằng Id
        public virtual  Map GetMapById(int mapId)
        {
            if (mapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            return _context.Maps.Include(m => m.Floor).FirstOrDefault(m => m.MapId == mapId);
        }

        // Cập nhật thông tin bản đồ
        public void UpdateMap(Map map)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));

            if (map.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            if (string.IsNullOrWhiteSpace(map.MapName))
                throw new ArgumentException("Map name cannot be null or empty.");

            if (map.MapImage2D == null)
                throw new ArgumentException("2D image cannot be null.");

            if (map.FloorId <= 0)
                throw new ArgumentException("Floor ID must be a positive integer.");

            var existingMap = _context.Maps.FirstOrDefault(m => m.MapId == map.MapId);
            if (existingMap != null)
            {
                existingMap.MapName = map.MapName;
                existingMap.MapImage2D = map.MapImage2D;
                existingMap.MapImage3D = map.MapImage3D;
                existingMap.FloorId = map.FloorId;
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Map with ID {map.MapId} does not exist.");
            }
        }

        // Xóa bản đồ bằng Id
        public void DeleteMap(int mapId)
        {
            if (mapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            var map = _context.Maps.FirstOrDefault(m => m.MapId == mapId);
            if (map != null)
            {
                // Xóa thông tin trong bảng Mapmanage
                var mapManages = _context.Mapmanages.Where(mm => mm.MapId == mapId);
                _context.Mapmanages.RemoveRange(mapManages);

                // Xóa bản đồ từ bảng Maps
                _context.Maps.Remove(map);

                // Lưu thay đổi
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException($"Map with ID {mapId} does not exist.");
            }
        }


        // Lấy danh sách tất cả các bản đồ
        public virtual List<Map> GetAllMaps()
        {
            return _context.Maps.Include(m => m.Floor).ToList();
        }

        // Tìm kiếm bản đồ theo tên
        public List<Map> SearchMapsByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Map>();

            var maps = _context.Maps
                .Where(m => m.MapName.Contains(keyword))
                .ToList();

            return maps;
        }
    }
}