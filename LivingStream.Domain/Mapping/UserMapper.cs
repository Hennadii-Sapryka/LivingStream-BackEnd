using AutoMapper;
using LivingStream.Data.Entities;
using LivingStream.Domain.Dto;
using LivingStream.Domain.Dto.User;

namespace LivingStream.Domain.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserFcmTokenDto, FcmToken>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UserEmailDto>().ReverseMap();

        }
    }
}
