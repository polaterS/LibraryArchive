using FluentValidation;
using LibraryArchive.Services.DTOs.BookShare;

namespace LibraryArchive.Services.Validation.BookShare
{
    public class BookShareDeleteDtoValidator : AbstractValidator<BookShareDeleteDto>
    {
        public BookShareDeleteDtoValidator()
        {
            RuleFor(x => x.BookShareId).GreaterThan(0).WithMessage("Book Share ID must be greater than 0.");
        }
    }
}
