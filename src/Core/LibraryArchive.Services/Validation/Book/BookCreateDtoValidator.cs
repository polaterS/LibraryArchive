using FluentValidation;
using LibraryArchive.Services.DTOs.Book;

namespace LibraryArchive.Services.Validation.Book
{
    public class BookCreateDtoValidator : AbstractValidator<BookCreateDto>
    {
        public BookCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required.");
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required.");
            RuleFor(x => x.CoverImageUrl).NotEmpty().WithMessage("Cover image URL is required.");
            RuleFor(x => x.ShelfLocation).NotEmpty().WithMessage("Shelf location is required.");
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
        }
    }
}
