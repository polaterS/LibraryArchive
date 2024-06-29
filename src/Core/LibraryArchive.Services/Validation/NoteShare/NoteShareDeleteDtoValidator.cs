using FluentValidation;
using LibraryArchive.Services.DTOs.NoteShare;

namespace LibraryArchive.Services.Validation.NoteShare
{
    public class NoteShareDeleteDtoValidator : AbstractValidator<NoteShareDeleteDto>
    {
        public NoteShareDeleteDtoValidator()
        {
            RuleFor(x => x.NoteShareId).GreaterThan(0).WithMessage("Note Share ID must be greater than 0.");
        }
    }
}
