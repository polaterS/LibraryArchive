using AutoMapper;
using LibraryArchive.Data.Entities;
using LibraryArchive.Services.DTOs.User;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.DTOs.NoteShare;
using LibraryArchive.Services.DTOs.BookShare;
using LibraryArchive.Services.DTOs.Category;
using LibraryArchive.Services.DTOs.Order;
using LibraryArchive.Services.DTOs.OrderDetail;
using LibraryArchive.Services.DTOs.Address;
using LibraryArchive.Services.DTOs.Notification;

namespace LibraryArchive.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<ApplicationUser, UserProfileDto>();
            CreateMap<UserProfileUpdateDto, ApplicationUser>();
            CreateMap<UserCreateDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserReadDto>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books))
                .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                .ForMember(dest => dest.Addresses, opt => opt.MapFrom(src => src.Addresses));
            CreateMap<UserUpdateDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));


            // Book Mappings
            CreateMap<Book, BookReadDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();

            // BookShare Mappings
            CreateMap<BookShare, BookShareReadDto>();
            CreateMap<BookShareCreateDto, BookShare>();
            CreateMap<BookShareUpdateDto, BookShare>();

            // Category Mappings
            CreateMap<Category, CategoryReadDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            // Note Mappings
            CreateMap<Note, NoteReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<NoteCreateDto, Note>();
            CreateMap<NoteUpdateDto, Note>();

            // NoteShare Mappings
            CreateMap<NoteShare, NoteShareReadDto>()
                .ForMember(dest => dest.NoteContent, opt => opt.MapFrom(src => src.Note.Content))
                .ForMember(dest => dest.SharedWithUserName, opt => opt.MapFrom(src => src.SharedWithUser.UserName));
            CreateMap<NoteShareCreateDto, NoteShare>();
            CreateMap<NoteShareUpdateDto, NoteShare>();

            // Order Mappings
            CreateMap<Order, OrderReadDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<OrderUpdateDto, Order>();

            // OrderDetail Mappings
            CreateMap<OrderDetail, OrderDetailReadDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<OrderDetailCreateDto, OrderDetail>();
            CreateMap<OrderDetailUpdateDto, OrderDetail>();

            // Address Mappings
            CreateMap<Address, AddressReadDto>();
            CreateMap<AddressCreateDto, Address>();
            CreateMap<AddressUpdateDto, Address>();

            // Notification Mappings
            CreateMap<Notification, NotificationReadDto>();
            CreateMap<NotificationCreateDto, Notification>();
            CreateMap<NotificationUpdateDto, Notification>();

            // NotificationSettings Mappings
            CreateMap<NotificationSettings, NotificationSettingsDto>().ReverseMap();
            CreateMap<NotificationSettingsCreateDto, NotificationSettings>().ReverseMap();
            CreateMap<NotificationSettingsUpdateDto, NotificationSettings>().ReverseMap();
        }
    }
}
