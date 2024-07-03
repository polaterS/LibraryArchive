using FluentValidation;
using LibraryArchive.Services.DTOs.Address;

namespace LibraryArchive.Services.Validation.Address
{
    public class AddressDeleteDtoValidator : AbstractValidator<AddressDeleteDto>
    {
        public AddressDeleteDtoValidator()
        {
            RuleFor(x => x.AddressId)
                .GreaterThan(0).WithMessage("Adres ID'si pozitif bir tamsayı olmalıdır.");
        }
    }
}
