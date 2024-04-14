﻿using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
            var building = _buildingDAO.GetBuildingById(buildingId);
            if (building == null)
            {
                throw new Exception("Building not found");
            }
            return _mapper.Map<BuildingDTO>(building);
        }

        public List<BuildingDTO> GetAllBuildings()
        {
            try
            {
                var buildings = _buildingDAO.GetAllBuildings();
                var buildingDTOs = buildings.Select(building => _mapper.Map<BuildingDTO>(building)).ToList();
                return buildingDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all buildings.", ex);
            }
        }

        public void AddBuilding(BuildingAddDTO building)
        {
            try
            {
                string uniqueFileName =  building.Image.FileName;

                var buildingModel = new Building
                {
                    BuildingName = building.BuildingName,
                    Status = building.Status,
                    FacilityId = building.FacilityId,
                    Image = uniqueFileName 
                };

                _buildingDAO.AddBuilding(buildingModel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding building.", ex);
            }
        }



        public void UpdateBuilding(BuildingUpdateDTO building)
        {
            try
            {
                string uniqueFileName = building.Image.FileName;

                var buildingModel = new Building
                {
                    BuildingId = building.BuildingId,
                    BuildingName = building.BuildingName,
                    Status = building.Status,
                    FacilityId = building.FacilityId,
                    Image = uniqueFileName
                };
                _buildingDAO.UpdateBuilding(_mapper.Map<Building>(buildingModel));
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating building.", ex);
            }
        }

        public void DeleteBuilding(int buildingId)
        {
            try
            {
                _buildingDAO.DeleteBuilding(buildingId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while deleting building.", ex);
            }
        }

      
    }
}
