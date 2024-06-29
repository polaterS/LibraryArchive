using FluentValidation;
using LibraryArchive.Services.DTOs.OrderDetail;

namespace LibraryArchive.Services.Validation.OrderDetail
{
    public class OrderDetailCreateDtoValidator : AbstractValidator<OrderDetailCreateDto>
    {
        public OrderDetailCreateDtoValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book ID must be greater than 0.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}
