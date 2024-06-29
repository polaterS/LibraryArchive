using FluentValidation;
using LibraryArchive.Services.DTOs.Category;

namespace LibraryArchive.Services.Validation.Category
{
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.CategoryId).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category name is required.");
        }
    }
}
