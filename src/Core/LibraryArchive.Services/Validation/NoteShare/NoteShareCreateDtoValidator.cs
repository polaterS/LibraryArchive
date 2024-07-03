using FluentValidation;
using LibraryArchive.Services.DTOs.NoteShare;

namespace LibraryArchive.Services.Validation.NoteShare
{
    public class NoteShareCreateDtoValidator : AbstractValidator<NoteShareCreateDto>
    {
        public NoteShareCreateDtoValidator()
        {
            RuleFor(x => x.NoteId)
                .GreaterThan(0).WithMessage("Not ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.SharedWithUserId)
                .NotEmpty().WithMessage("Paylaşılan kullanıcı ID'si gereklidir.");

            RuleFor(x => x.ShareType)
                .NotEmpty().WithMessage("Paylaşım türü gereklidir.")
                .Must(BeValidShareType).WithMessage("Geçersiz paylaşım türü.");
        }

        private bool BeValidShareType(string shareType)
        {
            var validShareTypes = new List<string> { "public", "private", "friends" };
            return validShareTypes.Contains(shareType);
        }
    }
}
