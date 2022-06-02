﻿using System.Collections.Generic;
using System.Threading.Tasks;
using LivingStream.Data.Entities;
using LivingStream.Domain.Dto.User;

namespace LivingStream.Domain.Dto
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);

        Task<IEnumerable<UserEmailDto>> GetAllUsersAsync();

        Task<UserDto> AcceptPolicyAsync();

        Task<UserFcmTokenDto?> AddUserFcmtokenAsync(UserFcmTokenDto userFcmtokenDto);

        Task DeleteUserFcmtokenAsync(string tokenToDelete);
        Task<CreateUserDto> AddUserAsync(CreateUserDto user);
    }
}
