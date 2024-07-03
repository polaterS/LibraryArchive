using LibraryArchive.API.Controllers;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryArchive.Tests.User
{
    public class UserControllerTests
    {
        private readonly UsersController _controller;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly Mock<UserManager<ApplicationUser>> _mockUserManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<UserService>> _mockLogger;
        private readonly UserService _userService;

        public UserControllerTests()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            _mockUserManager = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null);
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UserService>>();

            _userService = new UserService(
                _mockUserRepository.Object,
                _mockUserManager.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );

            _controller = new UsersController(_userService);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            // Arrange
            var users = new List<UserReadDto>
            {
                new UserReadDto { Id = "1", UserName = "user1", Email = "user1@example.com" },
                new UserReadDto { Id = "2", UserName = "user2", Email = "user2@example.com" }
            };
            _mockUserRepository.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(new List<ApplicationUser>());

            _mockMapper.Setup(m => m.Map<IEnumerable<UserReadDto>>(It.IsAny<IEnumerable<ApplicationUser>>()))
                .Returns(users);

            // Act
            var result = await _controller.GetAllUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUsers = Assert.IsType<List<UserReadDto>>(okResult.Value);
            Assert.Equal(2, returnUsers.Count);
        }

        [Fact]
        public async Task GetUserById_ReturnsOkResult_WithUser()
        {
            // Arrange
            var userId = "1";
            var user = new UserReadDto { Id = userId, UserName = "user1", Email = "user1@example.com" };
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(new ApplicationUser());

            _mockMapper.Setup(m => m.Map<UserReadDto>(It.IsAny<ApplicationUser>()))
                .Returns(user);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnUser = Assert.IsType<UserReadDto>(okResult.Value);
            Assert.Equal(userId, returnUser.Id);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "1";
            _mockUserRepository.Setup(service => service.GetByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"User with ID {userId} not found.", notFoundResult.Value);
        }

        [Fact]
        public async Task RegisterUser_ReturnsCreatedAtAction_WithCreatedUser()
        {
            // Arrange
            var userDto = new UserCreateDto { UserName = "user1", Email = "user1@example.com", Password = "Password123!" };
            var user = new ApplicationUser { Id = "1", UserName = "user1", Email = "user1@example.com" };
            _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mockMapper.Setup(m => m.Map<ApplicationUser>(It.IsAny<UserCreateDto>()))
                .Returns(user);

            _mockUserManager.Setup(um => um.FindByNameAsync(user.UserName))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.RegisterUser(userDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var createdUser = Assert.IsType<ApplicationUser>(createdAtActionResult.Value);
            Assert.Equal(userDto.UserName, createdUser.UserName);
        }

        //[Fact]
        //public async Task RegisterUser_ReturnsBadRequest_WhenModelStateIsInvalid()
        //{
        //    // Arrange
        //    var userDto = new UserCreateDto { UserName = "user1", Email = "user1@example.com", Password = "Password123!" };
        //    _controller.ModelState.AddModelError("Error", "Some error");

        //    _mockUserManager.Setup(um => um.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
        //        .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Model state is invalid" }));

        //    _mockMapper.Setup(m => m.Map<ApplicationUser>(It.IsAny<UserCreateDto>()))
        //        .Returns(new ApplicationUser { UserName = userDto.UserName, Email = userDto.Email });

        //    // Act
        //    var result = await _controller.RegisterUser(userDto);

        //    // Assert
        //    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //    Assert.IsType<SerializableError>(badRequestResult.Value);
        //}

        [Fact]
        public async Task UpdateUser_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = "1";
            var userDto = new UserUpdateDto { Id = userId, Email = "newemail@example.com", Name = "newname", Surname = "newsurname" };
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(new ApplicationUser());

            _mockUserManager.Setup(um => um.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.UpdateUser(userId, userDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateUser_ReturnsBadRequest_WhenUserIdMismatch()
        {
            // Arrange
            var userId = "1";
            var userDto = new UserUpdateDto { Id = "2", Email = "newemail@example.com", Name = "newname", Surname = "newsurname" };

            // Act
            var result = await _controller.UpdateUser(userId, userDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User ID mismatch", badRequestResult.Value);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var userId = "1";
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(new ApplicationUser());

            _mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteUser_ReturnsBadRequest_WhenUserNotFound()
        {
            // Arrange
            var userId = "1";
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync((ApplicationUser)null);

            _mockUserManager.Setup(um => um.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "User not found." }));

            // Act
            var result = await _controller.DeleteUser(userId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errors = Assert.IsType<List<IdentityError>>(badRequestResult.Value);
            Assert.Equal("User not found.", errors[0].Description);
        }
    }
}
