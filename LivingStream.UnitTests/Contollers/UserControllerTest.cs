using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using LivingStream.Domain.Dto;
using LivingStream.UnitTests.Base;
using LivingStream.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using LivingStream.Domain.Interfaces;

namespace LivingStream.UnitTests.Controllers
{
    public class UserControllerTest : TestBase
    {
        private readonly Mock<IUserService> userService;
        private readonly UserController userController;

        public UserControllerTest()
        {
            userService = new Mock<IUserService>();
            userController = new UserController(userService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserById_WhenUserExists_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            userService.Setup(service => service.GetUserByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(user);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetAllUsers_WhenUsersExist_ReturnsOkObjectResult(List<UserEmailDto> users)
        {
            // Arrange
            userService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await userController.GetAllUsers();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(users);
            }
        }

      

  
        
    }
}
