using BusinessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IBuildingRepository
    {
        BuildingDTO GetBuildingById(int buildingId);
        List<BuildingDTO> GetAllBuildings();
        void AddBuilding(BuildingAddDTO building);
        void UpdateBuilding(BuildingUpdateDTO building);
        void DeleteBuilding(int buildingId);

        /*  List<BuildingDTO> SearchBuildingsByName(string keyword);
          List<BuildingDTO> SearchBuildingsByStatus(string status);*/
    }
}
