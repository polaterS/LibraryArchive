using FluentValidation;
using LibraryArchive.Services.DTOs.Category;

namespace LibraryArchive.Services.Validation.Category
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori ismi gereklidir.")
                .MaximumLength(100).WithMessage("Kategori ismi en fazla 100 karakter uzunluğunda olabilir.");
        }
    }
}
