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

namespace LibraryArchive.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserProfileDto>();
            CreateMap<UserProfileUpdateDto, ApplicationUser>();
            CreateMap<UserCreateDto, ApplicationUser>();
            CreateMap<ApplicationUser, UserReadDto>();

            CreateMap<Book, BookReadDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();

            CreateMap<BookShare, BookShareReadDto>();
            CreateMap<BookShareCreateDto, BookShare>();
            CreateMap<BookShareUpdateDto, BookShare>();

            CreateMap<Book, BookReadDto>().ReverseMap();
            CreateMap<Category, CategoryReadDto>()
                .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();

            CreateMap<Note, NoteReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<NoteCreateDto, Note>();
            CreateMap<NoteUpdateDto, Note>();

            CreateMap<NoteShare, NoteShareReadDto>()
                .ForMember(dest => dest.NoteContent, opt => opt.MapFrom(src => src.Note.Content))
                .ForMember(dest => dest.SharedWithUserName, opt => opt.MapFrom(src => src.SharedWithUser.UserName));
            CreateMap<NoteShareCreateDto, NoteShare>();
            CreateMap<NoteShareUpdateDto, NoteShare>();

            CreateMap<Order, OrderReadDto>()
               .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<OrderCreateDto, Order>()
                .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<OrderUpdateDto, Order>();

            CreateMap<OrderDetail, OrderDetailReadDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<OrderDetailCreateDto, OrderDetail>();
            CreateMap<OrderDetailUpdateDto, OrderDetail>();
        }
    }
}
