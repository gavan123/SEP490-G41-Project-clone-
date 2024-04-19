using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MapManageDAO
    {
        private readonly finsContext _context;

        public MapManageDAO(finsContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddMapManage(Mapmanage mapManage)
        {
            if (mapManage == null)
            {
                throw new ArgumentNullException(nameof(mapManage));
            }

            _context.Mapmanages.Add(mapManage);
            _context.SaveChanges();
        }

        public Mapmanage GetMapManageById(int mapId)
        {
            if (mapId <= 0)
            {
                throw new ArgumentException("Invalid map ID", nameof(mapId));
            }

            return _context.Mapmanages.FirstOrDefault(m => m.MapId == mapId);
        }

        public void UpdateMapManage(Mapmanage mapManage)
        {
            if (mapManage == null)
            {
                throw new ArgumentNullException(nameof(mapManage));
            }

            var existingMapManage = _context.Mapmanages.FirstOrDefault(m => m.MapId == mapManage.MapId);

            if (existingMapManage != null)
            {
                existingMapManage.MemberId = mapManage.MemberId;
<<<<<<< HEAD
                existingMapManage.CreatedDate = mapManage.CreatedDate;
=======
                existingMapManage.CreateDate = mapManage.CreateDate;
>>>>>>> main
                existingMapManage.UpdateDate = mapManage.UpdateDate;

                _context.SaveChanges();
            }
        }

        public void DeleteMapManage(int mapId)
        {
            if (mapId <= 0)
            {
                throw new ArgumentException("Invalid map ID", nameof(mapId));
            }

            var mapManage = _context.Mapmanages.FirstOrDefault(m => m.MapId == mapId);
            if (mapManage != null)
            {
                _context.Mapmanages.Remove(mapManage);
                _context.SaveChanges();
            }
        }

        public List<Mapmanage> GetAllMapManages()
        {
            return _context.Mapmanages.ToList();
        }
    }
}
