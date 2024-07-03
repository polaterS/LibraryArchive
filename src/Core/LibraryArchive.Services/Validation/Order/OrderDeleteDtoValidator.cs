using FluentValidation;
using LibraryArchive.Services.DTOs.Order;

namespace LibraryArchive.Services.Validation.Order
{
    public class OrderDeleteDtoValidator : AbstractValidator<OrderDeleteDto>
    {
        public OrderDeleteDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş ID'si 0'dan büyük olmalıdır.");
        }
    }
}
