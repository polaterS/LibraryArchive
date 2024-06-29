using AutoMapper;
using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.Repositories.Interfaces;
using Serilog;

namespace LibraryArchive.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IValidator<UserCreateDto> _userCreateValidator;
        private readonly IValidator<UserUpdateDto> _userUpdateValidator;
        private readonly IValidator<UserDeleteDto> _userDeleteValidator;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IValidator<UserCreateDto> userCreateValidator,
            IValidator<UserUpdateDto> userUpdateValidator,
            IValidator<UserDeleteDto> userDeleteValidator)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = Log.ForContext<UserService>();
            _userCreateValidator = userCreateValidator;
            _userUpdateValidator = userUpdateValidator;
            _userDeleteValidator = userDeleteValidator;
        }

        public async Task<UserReadDto> GetUserByIdAsync(string userId)
        {
            try
            {
                _logger.Information("Getting user by ID: {UserId}", userId);
                var user = await _userRepository.GetUserByIdAsync(userId);
                return _mapper.Map<UserReadDto>(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting user by ID: {UserId}", userId);
                throw;
            }
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            try
            {
                _logger.Information("Getting all users");
                var users = await _userRepository.GetAllUsersAsync();
                return _mapper.Map<IEnumerable<UserReadDto>>(users);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error getting all users");
                throw;
            }
        }

        public async Task AddUserAsync(UserCreateDto userCreateDto)
        {
            await _userCreateValidator.ValidateAndThrowAsync(userCreateDto);
            try
            {
                var user = _mapper.Map<ApplicationUser>(userCreateDto);
                _logger.Information("Adding user: {User}", user);
                await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error adding user: {UserCreateDto}", userCreateDto);
                throw;
            }
        }

        public void RemoveUser(UserDeleteDto userDeleteDto)
        {
            _userDeleteValidator.ValidateAndThrow(userDeleteDto);
            try
            {
                var user = _mapper.Map<ApplicationUser>(userDeleteDto);
                _logger.Information("Removing user: {User}", user);
                _userRepository.RemoveUser(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error removing user: {UserDeleteDto}", userDeleteDto);
                throw;
            }
        }

        public void UpdateUser(UserUpdateDto userUpdateDto)
        {
            _userUpdateValidator.ValidateAndThrow(userUpdateDto);
            try
            {
                var user = _mapper.Map<ApplicationUser>(userUpdateDto);
                _logger.Information("Updating user: {User}", user);
                _userRepository.UpdateUser(user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error updating user: {UserUpdateDto}", userUpdateDto);
                throw;
            }
        }
    }
}
