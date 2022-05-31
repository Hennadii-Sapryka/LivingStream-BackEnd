using LivingStream.Data.Entities;
using LivingStream.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LivingStream.Data;
using Microsoft.Identity.Web.Resource;

namespace LivingStream.WebApi.Controllers
{
    [RequiredScope("ApiAccess")]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) =>
            this.userService = userService;

        /// <summary>
        /// Gets the user by identifier asynchronously.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) =>
            Ok(await userService.GetUserByIdAsync(id));

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All users</returns>
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers() =>
            Ok(await userService.GetAllUsersAsync());


        /// <summary>
        /// Accept privacy policy and terms of use for logged user.
        /// </summary>
        /// <returns>Updated user.</returns>
        [HttpPost("accept-policy")]
        public async Task<IActionResult> AcceptPrivacyPolicy()
        {
            var updatedUser = await userService.AcceptPolicyAsync();
            return Ok(updatedUser);
        }

        /// <summary>
        /// Adds fcmtoken to user asynchronously.
        /// </summary>
        /// <param name="userFCMTokenDto">fcm token to add.</param>
        /// <returns>Updated user.</returns>
        [HttpPost("fcmtoken")]
        public async Task<IActionResult> AddUserFcmtoken([FromForm] UserFcmTokenDto userFCMTokenDto) =>
            Ok(await userService.AddUserFcmtokenAsync(userFCMTokenDto));

        /// <summary>
        /// Removes fcmtoken from user asynchronously.
        /// </summary>
        /// <param name="token">fcm token to delete.</param>
        /// <returns>Updated user.</returns>
        [HttpDelete("fcmtoken/{token}")]
        public async Task<IActionResult> DeleteUserFcmtoken(string token)
        {
            await userService.DeleteUserFcmtokenAsync(token);
            return Ok();
        }
    }
}
