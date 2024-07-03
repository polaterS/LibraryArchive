using FluentValidation;
using LibraryArchive.Services.DTOs.Note;

namespace LibraryArchive.Services.Validation.Note
{
    public class NoteCreateDtoValidator : AbstractValidator<NoteCreateDto>
    {
        public NoteCreateDtoValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Kitap ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not içeriği gereklidir.")
                .MaximumLength(1000).WithMessage("Not içeriği en fazla 1000 karakter uzunluğunda olabilir.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si gereklidir.");
        }
    }
}
