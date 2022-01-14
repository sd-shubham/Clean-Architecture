using AutoMapper;
using App.Domain.Enities;
using App.Application.Dtos;

namespace App.Application.Mappings
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<GetUserDto, User>().ReverseMap();
        }
    }   
}
