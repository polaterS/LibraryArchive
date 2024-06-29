using FluentValidation;
using LibraryArchive.Services.DTOs.BookShare;

namespace LibraryArchive.Services.Validation.BookShare
{
    public class BookShareCreateDtoValidator : AbstractValidator<BookShareCreateDto>
    {
        public BookShareCreateDtoValidator()
        {
            RuleFor(x => x.NoteId).GreaterThan(0).WithMessage("Note ID must be greater than 0.");
            RuleFor(x => x.SharedWithUserId).NotEmpty().WithMessage("Shared with user ID is required.");
            RuleFor(x => x.ShareType).NotEmpty().WithMessage("Share type is required.");
        }
    }
}
