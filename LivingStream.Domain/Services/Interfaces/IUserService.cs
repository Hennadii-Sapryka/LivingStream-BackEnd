using System.Collections.Generic;
using System.Threading.Tasks;
using LivingStream.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetUserByIdAsync(int userId);

        Task<IEnumerable<UserEmailDto>> GetAllUsersAsync();

        Task<UserDto> AcceptPolicyAsync();

        Task<UserFcmTokenDto?> AddUserFcmtokenAsync(UserFcmTokenDto userFcmtokenDto);

        Task DeleteUserFcmtokenAsync(string tokenToDelete);
    }
}
