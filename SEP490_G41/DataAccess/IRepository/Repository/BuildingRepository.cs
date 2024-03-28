using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository.Repository
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly BuildingDAO _buildingDAO;
        private readonly IMapper _mapper;

        public BuildingRepository(BuildingDAO buildingDAO, IMapper mapper)
        {
            _buildingDAO = buildingDAO;
            _mapper = mapper;
        }

        public BuildingDTO GetBuildingById(int buildingId)
        {
            return _mapper.Map<BuildingDTO>(_buildingDAO.GetBuildingById(buildingId));
        }

        public List<BuildingDTO> GetAllBuildings()
        {
            var buildings = _buildingDAO.GetAllBuildings();
            var buildingDTOs = buildings.Select(building => _mapper.Map<BuildingDTO>(building)).ToList();
            return buildingDTOs;
        }

        public void AddBuilding(BuildingAddDTO building)
        {
            _buildingDAO.AddBuilding(_mapper.Map<Building>(building));
        }

        public void UpdateBuilding(BuildingUpdateDTO building)
        {
            _buildingDAO.UpdateBuilding(_mapper.Map<Building>(building));
        }

        public void DeleteBuilding(int buildingId)
        {
            _buildingDAO.DeleteBuilding(buildingId);
        }

        /*public List<BuildingDTO> SearchBuildingsByName(string keyword)
        {
            var buildings = _buildingDAO.SearchBuildingsByName(keyword);
            var buildingDTOs = buildings.Select(building => _mapper.Map<BuildingDTO>(building)).ToList();
            return buildingDTOs;
        }*/

    }
}
