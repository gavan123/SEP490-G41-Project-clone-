using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CreateMap<Facility, FacilityDTO>().ReverseMap();
            CreateMap<Facility, FacilityUpdateDTO>().ReverseMap();

            CreateMap<Floor, FloorAddDTO>().ReverseMap();
            CreateMap<Floor, FloorDTO>().ReverseMap();


            CreateMap<Map, MapDTO>()
               .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.FloorName)).ReverseMap();
            CreateMap<Map, MapAddDTO>().ReverseMap();
            CreateMap<Map, MapUpdateDTO>().ReverseMap();

            CreateMap<MapPointAddDTO, Mappoint>()
           .ForMember(dest => dest.LocationWeb, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationWeb))
           .ForMember(dest => dest.LocationApp, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationApp))
           .ForMember(dest => dest.LocationGps, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationGps));
            CreateMap<Mappoint, MapPointDTO>().ReverseMap();
            CreateMap<MapPointUpdateDTO, Mappoint>()
           .ForMember(dest => dest.LocationWeb, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationWeb))
           .ForMember(dest => dest.LocationApp, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationApp))
           .ForMember(dest => dest.LocationGps, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationGps));

            CreateMap<Member, MemberDTO>().ReverseMap();
<<<<<<< HEAD
            CreateMap<MapPointAddDTO, Mappoint>()
           .ForMember(dest => dest.LocationWeb, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationWeb))
           .ForMember(dest => dest.LocationApp, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationApp))
           .ForMember(dest => dest.LocationGps, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationGps));
            CreateMap<Mappoint, MapPointDTO>().ReverseMap();
            CreateMap<MapPointUpdateDTO, Mappoint>()
           .ForMember(dest => dest.LocationWeb, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationWeb))
           .ForMember(dest => dest.LocationApp, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationApp))
           .ForMember(dest => dest.LocationGps, opt => opt.ConvertUsing(new PointConverter(), src => src.LocationGps));
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
                return new Point(latitude, longitude);
            }

=======
<<<<<<< Updated upstream
=======
            CreateMap<Member, AddMemberDTO>().ReverseMap();
>>>>>>> Stashed changes
>>>>>>> minhpc

        }
    }
}
