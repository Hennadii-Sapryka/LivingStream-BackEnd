
using LivingStream.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Microsoft.Identity.Web.Resource;
using LivingStream.Domain.Dto.User;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using LivingStream.Domain.Interfaces;

namespace LivingStream.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;


        public UserController(IUserService userService) =>
            this.userService = userService;


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) =>
            Ok(await userService.GetUserByIdAsync(id));


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("all-users")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await userService.GetAllUsersAsync());
        }

        

        [HttpPost]
        public async Task<IActionResult> AddUser([FromForm] CreateUserDto user) =>
    Ok(await userService.AddUserAsync(user));

        [HttpPost("accept-policy")]
        public async Task<IActionResult> AcceptPrivacyPolicy()
        {
            var updatedUser = await userService.AcceptPolicyAsync();
            return Ok(updatedUser);
        }

        [HttpDelete]
        public async Task<bool> DeleteUser()
        {
            await userService.DeleteUserAsync();
            return true;
        }

        [HttpPost("fcmtoken")]
        public async Task<IActionResult> AddUserFcmtoken([FromForm] UserFcmTokenDto userFCMTokenDto) =>
            Ok(await userService.AddUserFcmtokenAsync(userFCMTokenDto));


        [HttpDelete("fcmtoken/{token}")]
        public async Task<IActionResult> DeleteUserFcmtoken(string token)
        {
            await userService.DeleteUserFcmtokenAsync(token);
            return Ok();
        }

    }
}
