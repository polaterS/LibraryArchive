using FluentValidation;
using LibraryArchive.Services.DTOs.Auth.Role;

namespace LibraryArchive.Services.Validation.Auth
{
    public class AssignRoleDtoValidator : AbstractValidator<AssignRoleDto>
    {
        public AssignRoleDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Rol adı zorunludur.")
                .Length(2, 50).WithMessage("Rol adı uzunluğu 2 ile 50 karakter arasında olmalıdır.");
        }
    }
}
