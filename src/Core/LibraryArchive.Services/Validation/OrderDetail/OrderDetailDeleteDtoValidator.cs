using FluentValidation;
using LibraryArchive.Services.DTOs.OrderDetail;

namespace LibraryArchive.Services.Validation.OrderDetail
{
    public class OrderDetailDeleteDtoValidator : AbstractValidator<OrderDetailDeleteDto>
    {
        public OrderDetailDeleteDtoValidator()
        {
            RuleFor(x => x.OrderDetailId).GreaterThan(0).WithMessage("Order Detail ID must be greater than 0.");
        }
    }
}
