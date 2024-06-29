using FluentValidation;
using LibraryArchive.Services.DTOs.Book;

namespace LibraryArchive.Services.Validation.Book
{
    public class BookDeleteDtoValidator : AbstractValidator<BookDeleteDto>
    {
        public BookDeleteDtoValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book ID must be greater than 0.");
        }
    }
}
