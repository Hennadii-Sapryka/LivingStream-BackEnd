using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LivingStream.Data.Entities;
using LivingStream.Data.Infrastructure;
using LivingStream.Domain.Dto;
using LivingStream.Domain.Dto.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using User = LivingStream.Data.Entities.User;

namespace LivingStream.Domain.Services
{
    public class LoginService
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;
        private static User user = new();

        public LoginService(IRepository<User> userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public Task<User> GetUserAsync(string email) =>
            userRepository.Query().FirstOrDefaultAsync(p => p.Email == email);


        public async Task<ActionResult<CreateUserDto>> Register(CreateUserDto request)
        {
            //var user = loginService.GetUserAsync(request.Email);
            //return Ok(await loginService.AddUserAsync(request));
           
            CreatePasswordHash(request.Name, out byte[] passwordHash, out byte[] passwordSalt);

            user.Name = request.Name;
            user.Email = request.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var userEntity = mapper.Map<CreateUserDto, User>(request);

            var newUser = await userRepository.AddAsync(userEntity);
            await userRepository.SaveChangesAsync();

            return mapper.Map<User, CreateUserDto>(newUser);
        }


        public async Task<CreateUserDto> AddUserAsync(CreateUserDto createUserModel)
        {
            var userEntity = mapper.Map<CreateUserDto, User>(createUserModel);

            var newUser = await userRepository.AddAsync(userEntity);
            await userRepository.SaveChangesAsync();

            return mapper.Map<User, CreateUserDto>(newUser);
        }

        public async Task<string> LoginAsync(string email)
        {
            var loggedInUser = await GetUserAsync(email);

            if (loggedInUser == null)
            {
                return "User not found";
            }

            string token = CreateToken(loggedInUser);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return token;
        }

        private async Task<User?> UpdateUserAsync(User? user)
        {
            if (user is null)
            {
                return user;
            }

            var updatedUser = await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return updatedUser;
        }


        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = httpContextAccessor.HttpContext!.Request.Cookies["refreshToken"];

            if (!user.RefreshToken.Equals(refreshToken))
            {
                return "Invalid Refresh Token.";
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return "Token expired.";
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return token;
        }

        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.Now.AddMinutes(20),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            httpContextAccessor.HttpContext!.Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                configuration.GetSection("JwtToken:Key").Value));

            var signigCredential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: configuration["JwtToken:Issuer"],
                audience: configuration["JwtToken:Audience"],
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(20),
                signigCredential);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


    }
}
