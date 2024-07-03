using FluentValidation;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.Validation.OrderDetail;

namespace LibraryArchive.Services.Validation.Order
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Sipariş detayları gereklidir.")
                .Must(details => details.All(d => d != null)).WithMessage("Geçersiz sipariş detayı.");

            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailUpdateDtoValidator());
        }
    }
}
