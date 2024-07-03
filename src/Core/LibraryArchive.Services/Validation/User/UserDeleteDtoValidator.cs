using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserDeleteDtoValidator : AbstractValidator<UserDeleteDto>
    {
        public UserDeleteDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kullanıcı ID'si gereklidir.");
        }
    }
}
