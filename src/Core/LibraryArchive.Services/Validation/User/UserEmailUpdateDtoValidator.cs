using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserEmailUpdateDtoValidator : AbstractValidator<UserEmailUpdateDto>
    {
        public UserEmailUpdateDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.").EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required.");
        }
    }
}
