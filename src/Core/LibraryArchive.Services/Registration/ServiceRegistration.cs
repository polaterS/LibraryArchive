using FluentValidation;
using LibraryArchive.Services.DTOs.Address;
using LibraryArchive.Services.DTOs.Auth.Login;
using LibraryArchive.Services.DTOs.Auth.Register;
using LibraryArchive.Services.DTOs.Auth.Role;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.DTOs.BookShare;
using LibraryArchive.Services.DTOs.Category;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.DTOs.NoteShare;
using LibraryArchive.Services.DTOs.Notification;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.Repositories;
using LibraryArchive.Services.Repositories.Concrete;
using LibraryArchive.Services.Repositories.Interfaces;
using LibraryArchive.Services.TaskManager.Concrete;
using LibraryArchive.Services.TaskManager.Interfaces;
using LibraryArchive.Services.Units;
using LibraryArchive.Services.Validation.Address;
using LibraryArchive.Services.Validation.Auth;
using LibraryArchive.Services.Validation.Book;
using LibraryArchive.Services.Validation.BookShare;
using LibraryArchive.Services.Validation.Category;
using LibraryArchive.Services.Validation.Note;
using LibraryArchive.Services.Validation.NoteShare;
using LibraryArchive.Services.Validation.Notification;
using LibraryArchive.Services.Validation.Order;
using LibraryArchive.Services.Validation.OrderDetail;
using LibraryArchive.Services.Validation.User;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryArchive.Services.Registration
{
    public static class ServiceRegistration
    {
        public static void AddLibraryArchiveServices(this IServiceCollection services)
        {
            // Validators

            //Address
            services.AddTransient<IValidator<AddressCreateDto>, AddressCreateDtoValidator>();
            services.AddTransient<IValidator<AddressDeleteDto>, AddressDeleteDtoValidator>();
            services.AddTransient<IValidator<AddressReadDto>, AddressReadDtoValidator>();
            services.AddTransient<IValidator<AddressUpdateDto>, AddressUpdateDtoValidator>();

            //Auth
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<RegisterDto>, RegisterDtoValidator>();
            services.AddTransient<IValidator<AssignRoleDto>, AssignRoleDtoValidator>();
            services.AddTransient<IValidator<RoleDto>, RoleDtoValidator>();

            //Book
            services.AddTransient<IValidator<BookCreateDto>, BookCreateDtoValidator>();
            services.AddTransient<IValidator<BookUpdateDto>, BookUpdateDtoValidator>();
            services.AddTransient<IValidator<BookDeleteDto>, BookDeleteDtoValidator>();

            //BookShare
            services.AddTransient<IValidator<BookShareCreateDto>, BookShareCreateDtoValidator>();
            services.AddTransient<IValidator<BookShareUpdateDto>, BookShareUpdateDtoValidator>();
            services.AddTransient<IValidator<BookShareDeleteDto>, BookShareDeleteDtoValidator>();

            //Category
            services.AddTransient<IValidator<CategoryCreateDto>, CategoryCreateDtoValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateDtoValidator>();
            services.AddTransient<IValidator<CategoryDeleteDto>, CategoryDeleteDtoValidator>();
            services.AddTransient<IValidator<CategoryReadDto>, CategoryReadDtoValidator>();

            //Note
            services.AddTransient<IValidator<NoteCreateDto>, NoteCreateDtoValidator>();
            services.AddTransient<IValidator<NoteUpdateDto>, NoteUpdateDtoValidator>();
            services.AddTransient<IValidator<NoteDeleteDto>, NoteDeleteDtoValidator>();
            services.AddTransient<IValidator<NoteReadDto>, NoteReadDtoValidator>();

            //NoteShare
            services.AddTransient<IValidator<NoteShareCreateDto>, NoteShareCreateDtoValidator>();
            services.AddTransient<IValidator<NoteShareUpdateDto>, NoteShareUpdateDtoValidator>();
            services.AddTransient<IValidator<NoteShareDeleteDto>, NoteShareDeleteDtoValidator>();
            services.AddTransient<IValidator<NoteShareReadDto>, NoteShareReadDtoValidator>();

            //Notification
            services.AddTransient<IValidator<NotificationCreateDto>, NotificationCreateDtoValidator>();
            services.AddTransient<IValidator<NotificationReadDto>, NotificationReadDtoValidator>();
            services.AddTransient<IValidator<NotificationSettingsCreateDto>, NotificationSettingsCreateDtoValidator>();
            services.AddTransient<IValidator<NotificationSettingsUpdateDto>, NotificationSettingsUpdateDtoValidator>();
            services.AddTransient<IValidator<NotificationUpdateDto>, NotificationUpdateDtoValidator>();

            //Order
            services.AddTransient<IValidator<OrderCreateDto>, OrderCreateDtoValidator>();
            services.AddTransient<IValidator<OrderUpdateDto>, OrderUpdateDtoValidator>();
            services.AddTransient<IValidator<OrderDeleteDto>, OrderDeleteDtoValidator>();
            services.AddTransient<IValidator<OrderReadDto>, OrderReadDtoValidator>();

            //OrderDetail
            services.AddTransient<IValidator<OrderDetailCreateDto>, OrderDetailCreateDtoValidator>();
            services.AddTransient<IValidator<OrderDetailUpdateDto>, OrderDetailUpdateDtoValidator>();

            //User
            services.AddTransient<IValidator<UserCreateDto>, UserCreateDtoValidator>();
            services.AddTransient<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
            services.AddTransient<IValidator<UserDeleteDto>, UserDeleteDtoValidator>();
            services.AddTransient<IValidator<UserEmailUpdateDto>, UserEmailUpdateDtoValidator>();
            services.AddTransient<IValidator<UserPasswordUpdateDto>, UserPasswordUpdateDtoValidator>();
            services.AddTransient<IValidator<UserProfileUpdateDto>, UserProfileUpdateDtoValidator>();

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
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationSettingsRepository, NotificationSettingsRepository>();
            services.AddScoped<INotificationSenderService, NotificationSenderService>();

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
            services.AddScoped<NotificationService>();
            services.AddScoped<NotificationSettingsService>();
        }
    }
}
