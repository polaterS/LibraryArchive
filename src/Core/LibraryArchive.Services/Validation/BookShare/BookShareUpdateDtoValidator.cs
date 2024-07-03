using FluentValidation;
using LibraryArchive.Services.DTOs.BookShare;

namespace LibraryArchive.Services.Validation.BookShare
{
    public class BookShareUpdateDtoValidator : AbstractValidator<BookShareUpdateDto>
    {
        public BookShareUpdateDtoValidator()
        {
            RuleFor(x => x.BookShareId)
                .GreaterThan(0).WithMessage("Kitap Paylaşım ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.ShareType)
                .NotEmpty().WithMessage("Paylaşım türü gereklidir.");
        }
    }
}
