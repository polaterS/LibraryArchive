using FluentValidation;
using LibraryArchive.Services.DTOs.BookShare;

namespace LibraryArchive.Services.Validation.BookShare
{
    public class BookShareUpdateDtoValidator : AbstractValidator<BookShareUpdateDto>
    {
        public BookShareUpdateDtoValidator()
        {
            RuleFor(x => x.BookShareId).GreaterThan(0).WithMessage("Book Share ID must be greater than 0.");
            RuleFor(x => x.ShareType).NotEmpty().WithMessage("Share type is required.");
        }
    }
}
