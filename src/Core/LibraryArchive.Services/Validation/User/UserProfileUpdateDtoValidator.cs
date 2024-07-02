using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserProfileUpdateDtoValidator : AbstractValidator<UserProfileUpdateDto>
    {
        public UserProfileUpdateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
        }
    }
}
