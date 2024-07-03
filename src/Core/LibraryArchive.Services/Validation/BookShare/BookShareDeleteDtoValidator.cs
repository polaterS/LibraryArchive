using FluentValidation;
using LibraryArchive.Services.DTOs.BookShare;

namespace LibraryArchive.Services.Validation.BookShare
{
    public class BookShareDeleteDtoValidator : AbstractValidator<BookShareDeleteDto>
    {
        public BookShareDeleteDtoValidator()
        {
            RuleFor(x => x.BookShareId)
                .GreaterThan(0).WithMessage("Kitap Paylaşım ID'si 0'dan büyük olmalıdır.");
        }
    }
}
