using System.Collections.Generic;
using System.Threading.Tasks;
using LivingStream.Data.Entities;
using LivingStream.Domain.Dto;
using LivingStream.Domain.Dto.User;

namespace LivingStream.Domain.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);

        Task<IEnumerable<UserEmailDto>> GetAllUsersAsync();

        Task<UserDto> AcceptPolicyAsync();

        Task<UserFcmTokenDto?> AddUserFcmtokenAsync(UserFcmTokenDto userFcmtokenDto);

        Task DeleteUserFcmtokenAsync(string tokenToDelete);
        Task <bool>DeleteUserAsync();
        Task<CreateUserDto> AddUserAsync(CreateUserDto user);
    }
}
