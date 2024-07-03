using FluentValidation;
using LibraryArchive.Services.DTOs.Auth.Register;

namespace LibraryArchive.Services.Validation.Auth
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı zorunludur.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("İsim alanı zorunludur.")
                .Length(2, 50).WithMessage("İsim uzunluğu 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyisim alanı zorunludur.")
                .Length(2, 50).WithMessage("Soyisim uzunluğu 2 ile 50 karakter arasında olmalıdır.");
        }
    }
}
