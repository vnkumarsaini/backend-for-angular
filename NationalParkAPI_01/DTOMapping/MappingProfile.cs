using AutoMapper;
using NationalParkAPI_01.Models;
using NationalParkAPI_01.Models.DTOs;

namespace NationalParkAPI_01.DTOMapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<NationalParkDto,NationalPark>().ReverseMap();
            CreateMap<TrailsDto,Trails>().ReverseMap();
            CreateMap<BookingsDto,Booking>().ReverseMap();
        }
    }
}
