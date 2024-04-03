using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.IRepository.Repository
{
    public class MapRepository : IMapRepository
    {
        private readonly MapDAO _mapDAO;
        private readonly IMapper _mapper;

        public MapRepository(MapDAO mapDAO, IMapper mapper)
        {
            _mapDAO = mapDAO;
            _mapper = mapper;
        }

        public MapDTO GetMapById(int mapId)
        {
            var map = _mapDAO.GetMapById(mapId);
            if (map == null)
            {
                throw new Exception("Map not found");
            }
            return _mapper.Map<MapDTO>(map);
        }

        public List<MapDTO> GetAllMaps()
        {
            try
            {
                var maps = _mapDAO.GetAllMaps();
                var mapDTOs = maps.Select(map => _mapper.Map<MapDTO>(map)).ToList();
                return mapDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all maps.", ex);
            }
        }

        public void AddMap(MapAddDTO map)
        {
            try
            {
                _mapDAO.AddMap(_mapper.Map<Map>(map));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map.", ex);
            }
        }

        public void UpdateMap(MapUpdateDTO map)
        {
            try
            {
                _mapDAO.UpdateMap(_mapper.Map<Map>(map));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map.", ex);
            }
        }

        public void DeleteMap(int mapId)
        {
            try
            {
                _mapDAO.DeleteMap(mapId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting map.", ex);
            }
        }

        // Các phương thức tìm kiếm và các phương thức khác có thể được thêm ở đây tùy thuộc vào yêu cầu cụ thể.
    }
}
