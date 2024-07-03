using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kullanıcı ID'si gereklidir.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email gereklidir.")
                .EmailAddress().WithMessage("Geçerli bir email adresi olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad gereklidir.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad gereklidir.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");
        }
    }
}
