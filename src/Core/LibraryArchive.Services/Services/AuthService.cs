using LibraryArchive.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryArchive.Services.Services
{
    public class AuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> CreateRoleAsync(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                return await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} already exists." });
        }

        public async Task<IdentityResult> AssignRoleAsync(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"User with email {email} not found." });
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Role {roleName} not found." });
            }

            return await _userManager.AddToRoleAsync(user, roleName);
        }
    }
}
