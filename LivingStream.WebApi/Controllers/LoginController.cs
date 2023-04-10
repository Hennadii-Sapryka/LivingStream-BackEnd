using LivingStream.Data.Entities;
using LivingStream.Domain;
using LivingStream.Domain.Dto;
using LivingStream.Domain.Dto.User;
using LivingStream.Domain.Services;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace LivingStream.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public static UserDto userDto = new();
        public static User user = new();
        private readonly LoginService loginService;

        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(CreateUserDto request)
        {
            var userr = await loginService.Register(request);
            //var user = loginService.GetUserAsync(request.Email);

            //return Ok(await loginService.AddUserAsync(request));

           // CreatePasswordHash(request.Name, out byte[] passwordHash, out byte[] passwordSalt);

            //user.Name = request.Name;
            //user.Email = request.Email;
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            return Ok(userr);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email) =>
            Ok(await loginService.LoginAsync(email));


        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken() =>
           Ok(await loginService.RefreshToken());
    }
}
