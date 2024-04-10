using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using static Google.Protobuf.Compiler.CodeGeneratorResponse.Types;

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
