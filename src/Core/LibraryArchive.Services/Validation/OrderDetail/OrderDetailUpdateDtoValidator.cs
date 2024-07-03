using FluentValidation;
using LibraryArchive.Services.DTOs.OrderDetail;

namespace LibraryArchive.Services.Validation.OrderDetail
{
    public class OrderDetailUpdateDtoValidator : AbstractValidator<OrderDetailUpdateDto>
    {
        public OrderDetailUpdateDtoValidator()
        {
            RuleFor(x => x.OrderDetailId)
                .GreaterThan(0).WithMessage("Sipariş Detayı ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Kitap ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır.");
        }
    }
}
