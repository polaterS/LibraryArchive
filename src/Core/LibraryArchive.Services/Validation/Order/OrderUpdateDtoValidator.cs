using FluentValidation;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.Validation.OrderDetail;

namespace LibraryArchive.Services.Validation.Order
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Order ID must be greater than 0.");
            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailUpdateDtoValidator());
        }
    }
}
