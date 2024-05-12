using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;

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
                var mapDTOs = maps.Select(m => new MapDTO
                {
                    MapId = m.MapId,
                    MapName = m.MapName,
                    MapImage2D = m.MapImage2D,
                    MapImage3D = m.MapImage3D,
                    FloorId = m.FloorId,
                    FloorName = m.Floor!.FloorName,
                    BuildingName = m.Floor.Building.BuildingName,
                    ManagerFullName = m.Mapmanages.FirstOrDefault()?.Member!.FullName,
                    BuildingImg = m.Floor.Building.Image,
                    BuildingId = m.Floor.Building.BuildingId
                }).ToList();

                return mapDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all maps.", ex);
            }
        }



        public void AddMap(MapAddDTO mapAddDTO, int memberId)
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

                _mapDAO.AddMap(map, memberId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map and map management.", ex);
            }
        }

        public void UpdateMap(MapUpdateDTO mapUpdateDTO)
        {

            try
            {
                _mapDAO.UpdateMap(mapUpdateDTO);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map.", ex);
            }
        }
        public string DeleteMap(int mapId)
        {
            try
            {
                return _mapDAO.DeleteMap(mapId);
            }
            catch (Exception ex)
            {
                return "Error occurred while deleting map: " + ex.Message;
            }

        }

    }
}