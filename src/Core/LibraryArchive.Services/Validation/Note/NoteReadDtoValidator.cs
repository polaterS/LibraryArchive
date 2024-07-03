using FluentValidation;
using LibraryArchive.Services.DTOs.Note;

namespace LibraryArchive.Services.Validation.Note
{
    public class NoteReadDtoValidator : AbstractValidator<NoteReadDto>
    {
        public NoteReadDtoValidator()
        {
            RuleFor(x => x.NoteId)
                .GreaterThan(0).WithMessage("Not ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Kitap ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not içeriği gereklidir.")
                .MaximumLength(1000).WithMessage("Not içeriği en fazla 1000 karakter uzunluğunda olabilir.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı gereklidir.");

            RuleFor(x => x.BookTitle)
                .NotEmpty().WithMessage("Kitap başlığı gereklidir.");
        }
    }
}
