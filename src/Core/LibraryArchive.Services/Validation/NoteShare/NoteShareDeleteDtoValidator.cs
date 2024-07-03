using FluentValidation;
using LibraryArchive.Services.DTOs.NoteShare;

namespace LibraryArchive.Services.Validation.NoteShare
{
    public class NoteShareDeleteDtoValidator : AbstractValidator<NoteShareDeleteDto>
    {
        public NoteShareDeleteDtoValidator()
        {
            RuleFor(x => x.NoteShareId)
                .GreaterThan(0).WithMessage("Not Paylaşım ID'si 0'dan büyük olmalıdır.");
        }
    }
}
