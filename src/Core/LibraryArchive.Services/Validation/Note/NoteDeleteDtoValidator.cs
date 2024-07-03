using FluentValidation;
using LibraryArchive.Services.DTOs.Note;

namespace LibraryArchive.Services.Validation.Note
{
    public class NoteDeleteDtoValidator : AbstractValidator<NoteDeleteDto>
    {
        public NoteDeleteDtoValidator()
        {
            RuleFor(x => x.NoteId)
                .GreaterThan(0).WithMessage("Not ID'si 0'dan büyük olmalıdır.");
        }
    }
}
