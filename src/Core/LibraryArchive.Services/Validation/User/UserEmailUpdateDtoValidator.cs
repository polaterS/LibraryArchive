using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserEmailUpdateDtoValidator : AbstractValidator<UserEmailUpdateDto>
    {
        public UserEmailUpdateDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Yeni email gereklidir.")
                .EmailAddress().WithMessage("Geçerli bir email adresi olmalıdır.");

            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre gereklidir.");
        }
    }
}
