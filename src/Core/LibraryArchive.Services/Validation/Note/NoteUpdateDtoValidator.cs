using FluentValidation;
using LibraryArchive.Services.DTOs.Note;

namespace LibraryArchive.Services.Validation.Note
{
    public class NoteUpdateDtoValidator : AbstractValidator<NoteUpdateDto>
    {
        public NoteUpdateDtoValidator()
        {
            RuleFor(x => x.NoteId).GreaterThan(0).WithMessage("Note ID must be greater than 0.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
            RuleFor(x => x.IsPrivate).NotNull().WithMessage("IsPrivate flag is required.");
        }
    }
}
