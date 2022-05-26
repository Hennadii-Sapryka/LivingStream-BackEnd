using LivingStream.Data.Entities;
using LivingStream.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LivingStream.WebApi.ServiceExtention
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetCurrentUserId(this ClaimsPrincipal principal, IRepository<User> userRepository)
        {
            if (principal == null)
            {
                throw new ArgumentNullException(nameof(principal));
            }

            var loggedInUserIdClaim = principal.Claims.First(c => c.Type == "preferred_username");

            return userRepository.Query().FirstOrDefault(u => u.Email == loggedInUserIdClaim.Value)!.Id;
        }
    }
}
