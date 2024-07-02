using FluentValidation;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.DTOs.BookShare;
using LibraryArchive.Services.DTOs.Category;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.DTOs.NoteShare;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.Repositories;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.Units;
using LibraryArchive.Services.Validation.Book;
using LibraryArchive.Services.Validation.BookShare;
using LibraryArchive.Services.Validation.Category;
using LibraryArchive.Services.Validation.Note;
using LibraryArchive.Services.Validation.NoteShare;
using LibraryArchive.Services.Validation.Order;
using LibraryArchive.Services.Validation.OrderDetail;
using LibraryArchive.Services.Validation.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryArchive.Services.Registration
{
    public static class ServiceRegistration
    {
        public static void AddLibraryArchiveServices(this IServiceCollection services)
        {
            // Validators
            services.AddTransient<IValidator<UserCreateDto>, UserCreateDtoValidator>();
            services.AddTransient<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
            services.AddTransient<IValidator<UserDeleteDto>, UserDeleteDtoValidator>();

            services.AddTransient<IValidator<BookCreateDto>, BookCreateDtoValidator>();
            services.AddTransient<IValidator<BookUpdateDto>, BookUpdateDtoValidator>();
            services.AddTransient<IValidator<BookDeleteDto>, BookDeleteDtoValidator>();

            services.AddTransient<IValidator<BookShareCreateDto>, BookShareCreateDtoValidator>();
            services.AddTransient<IValidator<BookShareUpdateDto>, BookShareUpdateDtoValidator>();
            services.AddTransient<IValidator<BookShareDeleteDto>, BookShareDeleteDtoValidator>();

            services.AddTransient<IValidator<CategoryCreateDto>, CategoryCreateDtoValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();
            services.AddTransient<IValidator<CategoryDeleteDto>, CategoryDeleteDtoValidator>();

            services.AddTransient<IValidator<NoteCreateDto>, NoteCreateDtoValidator>();
            services.AddTransient<IValidator<NoteUpdateDto>, NoteUpdateDtoValidator>();
            services.AddTransient<IValidator<NoteDeleteDto>, NoteDeleteDtoValidator>();

            services.AddTransient<IValidator<NoteShareCreateDto>, NoteShareCreateDtoValidator>();
            services.AddTransient<IValidator<NoteShareUpdateDto>, NoteShareUpdateDtoValidator>();
            services.AddTransient<IValidator<NoteShareDeleteDto>, NoteShareDeleteDtoValidator>();

            services.AddTransient<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
            services.AddTransient<IValidator<OrderUpdateDto>, OrderUpdateDtoValidator>();
            services.AddTransient<IValidator<OrderDeleteDto>, OrderDeleteDtoValidator>();

            services.AddTransient<IValidator<OrderDetailCreateDto>, OrderDetailCreateDtoValidator>();
            services.AddTransient<IValidator<OrderDetailUpdateDto>, OrderDetailUpdateDtoValidator>();
            services.AddTransient<IValidator<OrderDetailDeleteDto>, OrderDetailDeleteDtoValidator>();

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookShareRepository, BookShareRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            services.AddScoped<INoteShareRepository, NoteShareRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            // Add UserService to the service collection
            services.AddScoped<UserService>();
            services.AddScoped<AuthService>();
            services.AddScoped<BookService>();
            services.AddScoped<CategoryService>();
            services.AddScoped<BookShareService>();
            services.AddScoped<NoteService>();
            services.AddScoped<NoteShareService>();
            services.AddScoped<OrderService>();
            services.AddScoped<OrderDetailService>();
            services.AddScoped<AddressService>();
        }
    }
}
