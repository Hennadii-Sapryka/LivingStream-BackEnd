using AutoMapper;
using LivingStream.Data.Entities;
using LivingStream.Data.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using User = LivingStream.Data.Entities.User;

namespace LivingStream.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<FcmToken> fcmTokenRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IRepository<User> userRepository, IRepository<FcmToken> fcmTokenRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.fcmTokenRepository = fcmTokenRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);

            return mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserEmailDto>> GetAllUsersAsync()
        {
            var users = await userRepository.Query().ToListAsync();

            return mapper.Map<List<User>, List<UserEmailDto>>(users);
        }

        public async Task<UserDto> AcceptPolicyAsync()
        {
            var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);

            user.IsPolicyAccepted = true;
            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return mapper.Map<User, UserDto>(user);
        }

        public async Task<UserFcmTokenDto?> AddUserFcmtokenAsync(UserFcmTokenDto userFcmtokenDto)
        {
            var fcmToken = mapper.Map<UserFcmTokenDto, FcmToken>(userFcmtokenDto);
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var savedFcmToken = fcmTokenRepository.Query().FirstOrDefault(token => token.Token == fcmToken.Token);
            if (savedFcmToken != null)
            {
                savedFcmToken.UserId = userId;
                await fcmTokenRepository.SaveChangesAsync();
                return mapper.Map<FcmToken, UserFcmTokenDto>(savedFcmToken);
            }

            fcmToken.UserId = userId;
            fcmToken.Id = 0;
            var addedToken = await fcmTokenRepository.AddAsync(fcmToken);
            await fcmTokenRepository.SaveChangesAsync();

            return mapper.Map<FcmToken, UserFcmTokenDto>(addedToken);
        }

        public async Task DeleteUserFcmtokenAsync(string tokenToDelete)
        {
            var fcmToken = fcmTokenRepository.Query().FirstOrDefault(token => token.Token == tokenToDelete);

            if (fcmToken != null)
            {
                fcmTokenRepository.Delete(fcmToken);
                await fcmTokenRepository.SaveChangesAsync();
            }
        }
    }
}
