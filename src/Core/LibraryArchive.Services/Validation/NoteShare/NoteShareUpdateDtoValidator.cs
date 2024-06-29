using FluentValidation;
using LibraryArchive.Services.DTOs.NoteShare;

namespace LibraryArchive.Services.Validation.NoteShare
{
    public class NoteShareUpdateDtoValidator : AbstractValidator<NoteShareUpdateDto>
    {
        public NoteShareUpdateDtoValidator()
        {
            RuleFor(x => x.NoteShareId).GreaterThan(0).WithMessage("Note Share ID must be greater than 0.");
            RuleFor(x => x.ShareType).NotEmpty().WithMessage("Share type is required.");
        }
    }
}
