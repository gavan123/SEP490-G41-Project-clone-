using AutoMapper;
using BusinessObject.DTO;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
               .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.FloorName));
            CreateMap<MapDTO, Map>();

            // Định nghĩa ánh xạ từ MapAddDTO sang Map và ngược lại
            CreateMap<MapAddDTO, Map>();
            CreateMap<Map, MapAddDTO>();

            // Định nghĩa ánh xạ từ MapUpdateDTO sang Map và ngược lại
            CreateMap<MapUpdateDTO, Map>();
            CreateMap<Map, MapUpdateDTO>();

        }
    }
}
