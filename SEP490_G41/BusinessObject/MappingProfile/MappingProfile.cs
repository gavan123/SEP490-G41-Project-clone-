using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using NetTopologySuite.Geometries;
using System.Drawing;
using Point = NetTopologySuite.Geometries.Point;

namespace BusinessObject.MappingProfile
{
    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {

            CreateMap<Building, BuildingAddDTO>().ReverseMap();
            CreateMap<Building, BuildingDTO>()
               .ForMember(x => x.FacilityName, y => y.MapFrom(src => src.Facility.FacilityName)).ReverseMap();
            CreateMap<Building, BuildingUpdateDTO>().ReverseMap();

            CreateMap<Facility, FacilityAddDTO>().ReverseMap();
            CreateMap<Building, FacilityDTO>().ReverseMap();
            CreateMap<Facility, FacilityUpdateDTO>().ReverseMap();
            CreateMap<Map, MapDTO>()
               .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.FloorName));
            CreateMap<MapDTO, Map>();

            // Định nghĩa ánh xạ từ MapAddDTO sang Map và ngược lại
            CreateMap<MapAddDTO, Map>();
            CreateMap<Map, MapAddDTO>();

            // Định nghĩa ánh xạ từ MapUpdateDTO sang Map và ngược lại
            CreateMap<MapUpdateDTO, Map>();
            CreateMap<Map, MapUpdateDTO>();

            CreateMap<MapPointAddDTO, Mappoint>()
           .ForMember(dest => dest.Location, opt => opt.ConvertUsing(new PointConverter(), src => src.Location));
            CreateMap<Mappoint, MapPointDTO>().ReverseMap();
            CreateMap<MapPointUpdateDTO, Mappoint>()
           .ForMember(dest => dest.Location, opt => opt.ConvertUsing(new PointConverter(), src => src.Location));


        }

        public class PointConverter : IValueConverter<string, Point>
        {
            public Point Convert(string source, ResolutionContext context)
            {
                // Parse the location string to extract the coordinates
                string[] coordinates = source.Trim('[', ']').Split(',');
                double latitude = double.Parse(coordinates[0].Trim());
                double longitude = double.Parse(coordinates[1].Trim());

                // Create a new Point object
                return new Point(latitude,longitude );
            }
        }


    }
}
