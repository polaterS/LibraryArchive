using FluentValidation;
using LibraryArchive.Services.DTOs.Note;

namespace LibraryArchive.Services.Validation.Note
{
    public class NoteCreateDtoValidator : AbstractValidator<NoteCreateDto>
    {
        public NoteCreateDtoValidator()
        {
            RuleFor(x => x.BookId).GreaterThan(0).WithMessage("Book ID must be greater than 0.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.IsPrivate).NotNull().WithMessage("IsPrivate flag is required.");
        }
    }
}
