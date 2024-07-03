using FluentValidation;
using LibraryArchive.Services.DTOs.Order;

namespace LibraryArchive.Services.Validation.Order
{
    public class OrderReadDtoValidator : AbstractValidator<OrderReadDto>
    {
        public OrderReadDtoValidator()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0).WithMessage("Sipariş ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.OrderDate)
                .GreaterThan(DateTime.MinValue).WithMessage("Geçerli bir sipariş tarihi gereklidir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı gereklidir.");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("Sipariş detayları gereklidir.");
        }
    }
}
