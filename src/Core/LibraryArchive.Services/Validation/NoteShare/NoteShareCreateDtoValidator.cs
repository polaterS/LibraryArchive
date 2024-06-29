using FluentValidation;
using LibraryArchive.Services.DTOs.NoteShare;

namespace LibraryArchive.Services.Validation.NoteShare
{
    public class NoteShareCreateDtoValidator : AbstractValidator<NoteShareCreateDto>
    {
        public NoteShareCreateDtoValidator()
        {
            RuleFor(x => x.NoteId).GreaterThan(0).WithMessage("Note ID must be greater than 0.");
            RuleFor(x => x.SharedWithUserId).NotEmpty().WithMessage("Shared with user ID is required.");
            RuleFor(x => x.ShareType).NotEmpty().WithMessage("Share type is required.");
        }
    }
}
