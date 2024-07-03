using FluentValidation;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.Validation.OrderDetail;

namespace LibraryArchive.Services.Validation.Order
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailCreateDtoValidator());
        }
    }
}
