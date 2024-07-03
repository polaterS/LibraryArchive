using FluentValidation;
using LibraryArchive.Services.DTOs.BookShare;

namespace LibraryArchive.Services.Validation.BookShare
{
    public class BookShareCreateDtoValidator : AbstractValidator<BookShareCreateDto>
    {
        public BookShareCreateDtoValidator()
        {
            RuleFor(x => x.NoteId)
                .GreaterThan(0).WithMessage("Not ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.SharedWithUserId)
                .NotEmpty().WithMessage("Paylaşılan kullanıcı ID'si gereklidir.");
            RuleFor(x => x.ShareType)
                .NotEmpty().WithMessage("Paylaşım türü gereklidir.");
        }
    }
}
