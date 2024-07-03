using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserPasswordUpdateDtoValidator : AbstractValidator<UserPasswordUpdateDto>
    {
        public UserPasswordUpdateDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Mevcut şifre gereklidir.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifre gereklidir.")
                .MinimumLength(6).WithMessage("Yeni şifre en az 6 karakter olmalıdır.");
        }
    }
}
