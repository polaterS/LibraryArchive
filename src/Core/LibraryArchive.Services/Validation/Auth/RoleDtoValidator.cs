using FluentValidation;
using LibraryArchive.Services.DTOs.Auth.Role;

namespace LibraryArchive.Services.Validation.Auth
{
    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Rol adı zorunludur.")
                .Length(2, 50).WithMessage("Rol adı uzunluğu 2 ile 50 karakter arasında olmalıdır.");
        }
    }
}
