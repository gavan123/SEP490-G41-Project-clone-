using AutoMapper;
using BusinessObject.Models;
using DataAccess.DAO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json.Linq;


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
                var mapPointDTOs = mapPoints.Select(mapPoint =>
                {
                    var mapPointDTO = new MapPointDTO();
                    mapPointDTO.MapPointId = mapPoint.MapPointId;
                    mapPointDTO.MapId = mapPoint.MapId;
                    var geoJson = ConvertPointToGeoJson(mapPoint.Location);
                    var coordinatesJson = ExtractCoordinatesFromGeoJson(geoJson);
                    mapPointDTO.Location = coordinatesJson;
                    return mapPointDTO;
                }).ToList();

                return mapPointDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while getting all map points.", ex);
            }
        }
        private string ConvertPointToGeoJson(Point point)
        {
            var feature = new NetTopologySuite.Features.Feature(point, new AttributesTable());
            var writer = new GeoJsonWriter();
            return writer.Write(feature);
        }
        private string ExtractCoordinatesFromGeoJson(string geoJson)
        {
            // Parse chuỗi JSON thành đối tượng JObject
            var jObject = JObject.Parse(geoJson);

            // Trích xuất giá trị của trường 'coordinates' và chuyển đổi thành chuỗi
            string coordinatesJson = jObject["geometry"]["coordinates"].ToString();

            // Loại bỏ các ký tự xuống dòng và khoảng trắng
            coordinatesJson = coordinatesJson.Replace("\r\n", "").Replace(" ", "");

            return coordinatesJson;
        }
        public void AddMapPoint(MapPointAddDTO mapPoint)
        {
            if (mapPoint == null)
            {
                throw new ArgumentNullException(nameof(mapPoint), "MapPoint DTO cannot be null.");
            }

            try
            {
                // Parse the location string to extract the coordinates
                string[] coordinates = mapPoint.Location.Trim('[', ']').Split(',');
                double latitude = double.Parse(coordinates[0].Trim());
                double longitude = double.Parse(coordinates[1].Trim());

                // Create a new Point object and map the DTO to the entity
                Point location = new Point(latitude, longitude);
                var mapPointEntity = _mapper.Map<Mappoint>(mapPoint);
                mapPointEntity.Location = location;

                _mappointDAO.AddMappoint(mapPointEntity);
            }
            catch (MySqlConnector.MySqlException ex)
            {
                throw new Exception($"Error occurred while adding map point. Message: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while adding map point.", ex);
            }
        }

        public void UpdateMapPoint(MapPointUpdateDTO mapPoint)
        {
            if (mapPoint == null)
            {
                throw new ArgumentNullException(nameof(mapPoint), "MapPointUpdateDTO cannot be null.");
            }

            if (mapPoint.MapPointId <= 0)
            {
                throw new ArgumentException("Mappoint ID must be a positive integer.");
            }

            try
            {
                // Get the existing Mappoint entity
                var existingMapPoint = _mappointDAO.GetMappointById(mapPoint.MapPointId);
                if (existingMapPoint == null)
                {
                    throw new Exception($"MapPoint with ID {mapPoint.MapPointId} not found.");
                }

                // Update the properties of the existing entity
                string[] coordinates = mapPoint.Location.Trim('[', ']').Split(',');
                double latitude = double.Parse(coordinates[0].Trim());
                double longitude = double.Parse(coordinates[1].Trim());
                existingMapPoint.Location = new Point(longitude, latitude);
                existingMapPoint.MapId = mapPoint.MapId;

                // Update the entity in the database
                _mappointDAO.UpdateMappoint(existingMapPoint);
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while updating map point.", ex);
            }
        }
        public void DeleteMapPoint(int mapPointId)
        {
            if (mapPointId <= 0)
            {
                throw new ArgumentException("Mappoint ID must be a positive integer.");
            }

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
