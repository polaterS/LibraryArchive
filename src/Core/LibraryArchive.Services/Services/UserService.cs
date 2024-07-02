using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LibraryArchive.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<(IdentityResult, ApplicationUser)> RegisterUserAsync(UserCreateDto userDto)
        {
            var user = _mapper.Map<ApplicationUser>(userDto);
            user.IsActive = true;

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                var createdUser = await _userManager.FindByNameAsync(user.UserName);
                return (result, createdUser);
            }
            return (result, null);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }


        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }



        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user != null)
            {
                _mapper.Map(userDto, user);
                var result = await _userManager.UpdateAsync(user);
                return result;
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        public async Task<IdentityResult> DeleteUserAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                return result;
            }
            return IdentityResult.Failed(new IdentityError { Description = "User not found." });
        }

        public async Task<IEnumerable<UserReadDto>> SearchUsersAsync(string searchTerm)
        {
            var users = await _userRepository.SearchUsersAsync(searchTerm);
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<IEnumerable<UserReadDto>> FilterUsersByRoleAsync(string role, bool isActive)
        {
            var users = await _userRepository.FilterUsersAsync(role, isActive);
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserProfileDto> GetUserProfileAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError($"User not found with Email {email}");
                return null;
            }
            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(string email, UserProfileUpdateDto userProfileUpdateDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError($"User not found with Email {email}");
                return false;
            }

            user.Name = userProfileUpdateDto.Name;
            user.Surname = userProfileUpdateDto.Surname;
            user.ProfilePictureUrl = userProfileUpdateDto.ProfilePictureUrl;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserEmailAsync(string email, UserEmailUpdateDto userEmailUpdateDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError($"User not found with Email {email}");
                return false;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, userEmailUpdateDto.CurrentPassword);
            if (!passwordCheck)
            {
                _logger.LogError($"Incorrect current password for user with Email {email}");
                return false;
            }

            user.Email = userEmailUpdateDto.Email;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<IdentityResult> UpdateUserPasswordAsync(string email, UserPasswordUpdateDto userPasswordUpdateDto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogError($"User not found with Email {email}");
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });
            }

            var result = await _userManager.ChangePasswordAsync(user, userPasswordUpdateDto.CurrentPassword, userPasswordUpdateDto.NewPassword);
            return result;
        }
    }
}
