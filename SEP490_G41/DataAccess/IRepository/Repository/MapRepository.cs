using AutoMapper;
using AutoMapper.Execution;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;

namespace DataAccess.IRepository.Repository
{
    public class MapRepository : IMapRepository
    {
        private readonly MapDAO _mapDAO;
        private readonly IMapper _mapper;
        private readonly FloorDAO _floorDAO;
        private readonly BuildingDAO _buildingDAO;
        private readonly MemberDAO _memberDAO;
        private readonly MapManageDAO _mapmanageDAO;



        public MapRepository(MapDAO mapDAO, IMapper mapper, FloorDAO floorDAO, BuildingDAO buildingDAO, MemberDAO memberDAO,MapManageDAO mapManageDAO)
        {
            _mapDAO = mapDAO;
            _mapper = mapper;
            _floorDAO = floorDAO;
            _buildingDAO = buildingDAO;
            _memberDAO = memberDAO;
            _mapmanageDAO = mapManageDAO;
        }

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
                var mapDTOs = (from m in maps
                               join mm in _mapmanageDAO.GetAllMapManages() on m.MapId equals mm.MapId
                               join mem in _memberDAO.GetAllMembers() on mm.MemberId equals mem.MemberId
                               join f in _floorDAO.GetAllFloors() on m.FloorId equals f.FloorId
                               join b in _buildingDAO.GetAllBuildings() on f.BuildingId equals b.BuildingId
                               select new MapDTO
                               {
                                   MapId = m.MapId,
                                   MapName = m.MapName,
                                   MapImage2D = m.MapImage2D,
                                   MapImage3D = m.MapImage3D,
                                   FloorId = m.FloorId,
                                   FloorName = f.FloorName,
                                   BuildingName = b.BuildingName,
                                   ManagerFullName = mem.FullName,
                                   BuildingImg = b.Image,
                               }).ToList();

                return mapDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all maps.", ex);
            }
        }


        public void AddMap(MapAddDTO mapAddDTO, BusinessObject.Models.Member member)
        {
            if (mapAddDTO == null)
                throw new ArgumentNullException(nameof(mapAddDTO));
            try
            {
                string uniqueFileName = mapAddDTO.MapImage2D.FileName;

                var map = new Map
                {
                    MapName = mapAddDTO.MapName,
                    MapImage2D = uniqueFileName,
                    FloorId = mapAddDTO.FloorId
                };

                _mapDAO.AddMap(map, member);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map and map management.", ex);
            }
        }

        public void UpdateMap(MapUpdateDTO mapUpdateDTO)
        {
            if (mapUpdateDTO == null)
                throw new ArgumentNullException(nameof(mapUpdateDTO));

            if (mapUpdateDTO.MapId <= 0)
                throw new ArgumentException("Map ID must be a positive integer.");
            try
            {
                string uniqueFileName = mapUpdateDTO.MapImage2D.FileName;

                var map = new Map
                {
                    MapId = mapUpdateDTO.MapId,
                    MapName = mapUpdateDTO.MapName,
                    MapImage2D = uniqueFileName,
                    FloorId = mapUpdateDTO.FloorId
                };

                _mapDAO.UpdateMap(map);
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

        public void AddMap(MapAddDTO map)
        {
            
        }
    }
}