using LibraryArchive.API.Controllers;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services;
using LibraryArchive.Services.DTOs.Auth.Login;
using LibraryArchive.Services.DTOs.Auth.Register;
using LibraryArchive.Services.DTOs.Auth.Role;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace LibraryArchive.Tests.Auth
{
    public class AuthControllerTests
    {
        private readonly AuthController _authController;
        private readonly Mock<AuthService> _authServiceMock;

        public AuthControllerTests()
        {
            var userManagerMock = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                null, null, null, null);

            var roleManagerMock = new Mock<RoleManager<IdentityRole>>(
                Mock.Of<IRoleStore<IdentityRole>>(), null, null, null, null);

            var configurationMock = new Mock<IConfiguration>();
            configurationMock.SetupGet(c => c["Jwt:Key"]).Returns("some_secret_key");
            configurationMock.SetupGet(c => c["Jwt:Issuer"]).Returns("issuer");
            configurationMock.SetupGet(c => c["Jwt:Audience"]).Returns("audience");

            _authServiceMock = new Mock<AuthService>(userManagerMock.Object, signInManagerMock.Object, roleManagerMock.Object, configurationMock.Object);
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Register_Should_Return_Token_On_Success()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@example.com", Password = "Password123!", Name = "Test", Surname = "User" };
            _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterDto>())).ReturnsAsync("test_token");

            // Act
            var result = await _authController.Register(registerDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenResponse = Assert.IsType<TokenResponse>(okResult.Value);
            Assert.Equal("test_token", tokenResponse.Token);
        }

        [Fact]
        public async Task Register_Should_Return_BadRequest_On_Failure()
        {
            // Arrange
            var registerDto = new RegisterDto { Email = "test@example.com", Password = "Password123!", Name = "Test", Surname = "User" };
            _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterDto>())).ThrowsAsync(new Exception("User creation failed"));

            // Act
            var result = await _authController.Register(registerDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User creation failed", badRequestResult.Value);
        }

        [Fact]
        public async Task Login_Should_Return_Token_On_Success()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123!" };
            _authServiceMock.Setup(s => s.LoginAsync(It.IsAny<LoginDto>())).ReturnsAsync("test_token");

            // Act
            var result = await _authController.Login(loginDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenResponse = Assert.IsType<TokenResponse>(okResult.Value);
            Assert.Equal("test_token", tokenResponse.Token);
        }

        [Fact]
        public async Task Login_Should_Return_BadRequest_On_Failure()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "Password123!" };
            _authServiceMock.Setup(s => s.LoginAsync(It.IsAny<LoginDto>())).ThrowsAsync(new Exception("Invalid email or password"));

            // Act
            var result = await _authController.Login(loginDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Invalid email or password", badRequestResult.Value);
        }

        [Fact]
        public async Task AssignRole_Should_Return_Ok_On_Success()
        {
            // Arrange
            var assignRoleDto = new AssignRoleDto { Email = "test@example.com", RoleName = "Admin" };
            _authServiceMock.Setup(s => s.AssignRoleAsync(It.IsAny<AssignRoleDto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _authController.AssignRole(assignRoleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rol başarıyla atandı.", okResult.Value);
        }

        [Fact]
        public async Task AssignRole_Should_Return_BadRequest_On_Failure()
        {
            // Arrange
            var assignRoleDto = new AssignRoleDto { Email = "test@example.com", RoleName = "Admin" };
            _authServiceMock.Setup(s => s.AssignRoleAsync(It.IsAny<AssignRoleDto>())).ThrowsAsync(new Exception("User not found"));

            // Act
            var result = await _authController.AssignRole(assignRoleDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User not found", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateRole_Should_Return_Ok_On_Success()
        {
            // Arrange
            var roleDto = new RoleDto { RoleName = "Admin" };
            _authServiceMock.Setup(s => s.CreateRoleAsync(It.IsAny<RoleDto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _authController.CreateRole(roleDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Rol başarıyla oluşturuldu", okResult.Value);
        }

        [Fact]
        public async Task CreateRole_Should_Return_BadRequest_On_Failure()
        {
            // Arrange
            var roleDto = new RoleDto { RoleName = "Admin" };
            _authServiceMock.Setup(s => s.CreateRoleAsync(It.IsAny<RoleDto>())).ThrowsAsync(new Exception("Role already exists"));

            // Act
            var result = await _authController.CreateRole(roleDto);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Role already exists", badRequestResult.Value);
        }
    }
}
