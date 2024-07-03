using FluentValidation;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.Validation.Address;
using LibraryArchive.Services.Validation.OrderDetail;

namespace LibraryArchive.Services.Validation.Order
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Sipariş detayları gereklidir.")
                .Must(details => details.All(d => d != null)).WithMessage("Geçersiz sipariş detayı.");

            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailCreateDtoValidator());

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Adres bilgisi gereklidir.")
                .SetValidator(new AddressCreateDtoValidator());
        }
    }
}
