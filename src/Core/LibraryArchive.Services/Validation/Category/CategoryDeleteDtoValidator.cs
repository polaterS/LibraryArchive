using FluentValidation;
using LibraryArchive.Services.DTOs.Category;

namespace LibraryArchive.Services.Validation.Category
{
    public class CategoryDeleteDtoValidator : AbstractValidator<CategoryDeleteDto>
    {
        public CategoryDeleteDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori ID'si 0'dan büyük olmalıdır.");
        }
    }
}
