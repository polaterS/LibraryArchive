using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserPasswordUpdateDtoValidator : AbstractValidator<UserPasswordUpdateDto>
    {
        public UserPasswordUpdateDtoValidator()
        {
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required.");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password is required.").MinimumLength(6).WithMessage("New password must be at least 6 characters.");
        }
    }
}
