using FluentValidation;
using LibraryArchive.Services.DTOs.Book;

namespace LibraryArchive.Services.Validation.Book
{
    public class BookUpdateDtoValidator : AbstractValidator<BookUpdateDto>
    {
        public BookUpdateDtoValidator()
        {
            RuleFor(x => x.BookId)
                .GreaterThan(0).WithMessage("Kitap ID'si pozitif bir sayı olmalıdır.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Kitap başlığı alanı zorunludur.")
                .Length(1, 200).WithMessage("Kitap başlığı 1 ile 200 karakter arasında olmalıdır.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Yazar adı alanı zorunludur.")
                .Length(1, 100).WithMessage("Yazar adı 1 ile 100 karakter arasında olmalıdır.");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN alanı zorunludur.")
                .Length(10, 13).WithMessage("ISBN 10 ile 13 karakter arasında olmalıdır.");

            RuleFor(x => x.CoverImageUrl)
                .NotEmpty().WithMessage("Kapak resmi URL alanı zorunludur.")
                .Must(BeAValidUrl).WithMessage("Geçerli bir URL giriniz.");

            RuleFor(x => x.ShelfLocation)
                .NotEmpty().WithMessage("Raf yeri alanı zorunludur.")
                .Length(1, 50).WithMessage("Raf yeri 1 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kategori ID'si pozitif bir sayı olmalıdır.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
