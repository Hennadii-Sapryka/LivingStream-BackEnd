using AutoMapper;
using LivingStream.Data.Entities;

namespace LivingStream.Domain.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserFcmTokenDto, FcmToken>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserEmailDto>().ReverseMap();
        }
    }
}
