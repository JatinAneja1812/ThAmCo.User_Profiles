using AutoMapper;
using ThAmCo.User_Profiles.DTOs;
using ThAmCo.User_Profiles.Models;

namespace ThAmCo.User_Profiles.Automapper
{
    public class UserDataMappingProfile : Profile
    {
        public UserDataMappingProfile()
        {
            CreateMap<User, UserProfilesDTO>()
            .ReverseMap();
        }
    }
}
