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
            if (mapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            var map = _mapDAO.GetMapById(mapId);
            if (map == null)
                throw new Exception("Map not found");

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
            if (map == null)
                throw new ArgumentNullException(nameof(map));
            string uniqueFileName = map.Image2D.FileName;
            try
            {
                // Map DTO properties to entity properties manually
                var newMap = new Map
                {
                    MapName = map.MapName,
                    Image2D = uniqueFileName,
                    Image3D = "Alpha.jpg",
                    FloorId = map.FloorId
                };

                _mapDAO.AddMap(newMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map.", ex);
            }
        }

        public void UpdateMap(MapUpdateDTO map)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));

            if (map.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");
            string uniqueFileName = map.Image2D.FileName;
            try
            {
                // Map DTO properties to entity properties manually
                var updatedMap = new Map
                {
                    MapId = map.MapId,
                    MapName = map.MapName,
                    Image2D = uniqueFileName,
                    Image3D = "Alpha.jpg",
                    FloorId = map.FloorId
                };

                _mapDAO.UpdateMap(updatedMap);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map.", ex);
            }
        }

        public void DeleteMap(int mapId)
        {
            if (mapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");

            try
            {
                _mapDAO.DeleteMap(mapId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting map.", ex);
            }
        }
    }
}