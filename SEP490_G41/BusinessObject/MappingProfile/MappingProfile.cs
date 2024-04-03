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
