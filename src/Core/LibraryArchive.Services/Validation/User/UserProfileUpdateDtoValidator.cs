using FluentValidation;
using LibraryArchive.Services.DTOs.User;

namespace LibraryArchive.Services.Validation.User
{
    public class UserProfileUpdateDtoValidator : AbstractValidator<UserProfileUpdateDto>
    {
        public UserProfileUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Ad gereklidir.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad gereklidir.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            RuleFor(x => x.ProfilePictureUrl)
                .Must(BeAValidUrl).WithMessage("Geçerli bir URL olmalıdır.")
                .When(x => !string.IsNullOrEmpty(x.ProfilePictureUrl));
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
