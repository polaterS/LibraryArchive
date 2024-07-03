using FluentValidation;
using LibraryArchive.Services.DTOs.Address;

namespace LibraryArchive.Services.Validation.Address
{
    public class AddressReadDtoValidator : AbstractValidator<AddressReadDto>
    {
        public AddressReadDtoValidator()
        {
            RuleFor(x => x.AddressId)
                .GreaterThan(0).WithMessage("Adres ID'si pozitif bir tamsayı olmalıdır.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si zorunludur.");

            RuleFor(x => x.Street)
                .NotEmpty().WithMessage("Sokak alanı zorunludur.")
                .Length(2, 100).WithMessage("Sokak uzunluğu 2 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir alanı zorunludur.")
                .Length(2, 50).WithMessage("Şehir uzunluğu 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("İlçe alanı zorunludur.")
                .Length(2, 50).WithMessage("Eyalet uzunluğu 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Posta kodu alanı zorunludur.")
                .Matches(@"^\d{5}$").WithMessage("Posta kodu 5 haneli bir sayı olmalıdır.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke alanı zorunludur.")
                .Length(2, 50).WithMessage("Ülke uzunluğu 2 ile 50 karakter arasında olmalıdır.");
        }
    }
}
