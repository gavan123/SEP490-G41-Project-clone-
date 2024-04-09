using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.IRepository.Repository
{
    public class MapPointRepository : IMapPointRepository
    {
        private readonly MappointDAO _mappointDAO;
        private readonly IMapper _mapper;

        public MapPointRepository(MappointDAO mappointDAO, IMapper mapper)
        {
            _mappointDAO = mappointDAO;
            _mapper = mapper;
        }

        public MapPointDTO GetMapPointById(int mapPointId)
        {
            var mapPoint = _mappointDAO.GetMappointById(mapPointId);
            if (mapPoint == null)
            {
                throw new Exception("MapPoint not found");
            }
            return _mapper.Map<MapPointDTO>(mapPoint);
        }

        public List<MapPointDTO> GetAllMapPoints()
        {
            try
            {
                var mapPoints = _mappointDAO.GetAllMappoints();
                var mapPointDTOs = mapPoints.Select(mapPoint => _mapper.Map<MapPointDTO>(mapPoint)).ToList();
                return mapPointDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }

        public void AddMapPoint(MapPointAddDTO mapPoint)
        {
            try
            {
                _mappointDAO.AddMappoint(_mapper.Map<Mappoint>(mapPoint));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map point.", ex);
            }
        }

        public void UpdateMapPoint(MapPointUpdateDTO mapPoint)
        {
            try
            {
                _mappointDAO.UpdateMappoint(_mapper.Map<Mappoint>(mapPoint));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map point.", ex);
            }
        }

        public void DeleteMapPoint(int mapPointId)
        {
            try
            {
                _mappointDAO.DeleteMappoint(mapPointId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting map point.", ex);
            }
        }
    }
}
