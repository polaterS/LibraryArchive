using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace LibraryArchive.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<(IdentityResult, ApplicationUser)> RegisterUserAsync(UserCreateDto userDto)
        {
            var user = new ApplicationUser
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Name = userDto.Name,
                Surname = userDto.Surname,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                // Kullanıcı başarıyla oluşturulduktan sonra, UserManager kullanılarak kullanıcıyı tekrar buluyoruz.
                var createdUser = await _userManager.FindByNameAsync(user.UserName);
                return (result, createdUser);
            }
            return (result, null);
        }


        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userDto)
        {
            var user = await _userRepository.GetByIdAsync(userDto.Id);
            if (user != null)
            {
                user.Email = userDto.Email;
                user.Name = userDto.Name;
                user.Surname = userDto.Surname;

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

        public async Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm)
        {
            return await _userRepository.SearchUsersAsync(searchTerm);
        }

        public async Task<IEnumerable<ApplicationUser>> FilterUsersByRoleAsync(string role, bool isActive)
        {
            return await _userRepository.FilterUsersAsync(role, isActive);
        }
    }
}
