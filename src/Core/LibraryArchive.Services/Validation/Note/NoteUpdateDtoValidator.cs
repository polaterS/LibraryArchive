﻿using FluentValidation;
using LibraryArchive.Services.DTOs.Note;

namespace LibraryArchive.Services.Validation.Note
{
    public class NoteUpdateDtoValidator : AbstractValidator<NoteUpdateDto>
    {
        public NoteUpdateDtoValidator()
        {
            RuleFor(x => x.NoteId)
                .GreaterThan(0).WithMessage("Not ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Not içeriği gereklidir.")
                .MaximumLength(1000).WithMessage("Not içeriği en fazla 1000 karakter uzunluğunda olabilir.");
        }
    }
}
