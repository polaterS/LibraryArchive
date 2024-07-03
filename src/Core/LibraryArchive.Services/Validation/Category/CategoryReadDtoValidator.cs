using FluentValidation;
using LibraryArchive.Services.DTOs.Category;

namespace LibraryArchive.Services.Validation.Category
{
    public class CategoryReadDtoValidator : AbstractValidator<CategoryReadDto>
    {
        public CategoryReadDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori ismi gereklidir.")
                .MaximumLength(100).WithMessage("Kategori ismi en fazla 100 karakter uzunluğunda olabilir.");

            RuleFor(x => x.BooksCount)
                .GreaterThanOrEqualTo(0).WithMessage("Kitap sayısı 0 veya daha büyük olmalıdır.");
        }
    }
}
