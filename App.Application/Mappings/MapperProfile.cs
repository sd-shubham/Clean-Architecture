using AutoMapper;
using App.Application.Services;
using App.Domain.Enities;

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
