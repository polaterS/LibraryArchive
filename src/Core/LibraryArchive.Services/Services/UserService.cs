using AutoMapper;
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
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _mapper = mapper;
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

        public async Task<UserReadDto> GetUserByIdAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<UserReadDto>(user);
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
    }
}
