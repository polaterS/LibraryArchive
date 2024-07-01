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
            CreateMap<ApplicationUser, UserReadDto>();
            CreateMap<UserCreateDto, ApplicationUser>();
            CreateMap<UserUpdateDto, ApplicationUser>();
            CreateMap<UserDeleteDto, ApplicationUser>();

            CreateMap<Book, BookReadDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<BookCreateDto, Book>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<BookDeleteDto, Book>();

            CreateMap<Note, NoteReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<NoteCreateDto, Note>();
            CreateMap<NoteUpdateDto, Note>();
            CreateMap<NoteDeleteDto, Note>();

            CreateMap<NoteShare, NoteShareReadDto>()
                .ForMember(dest => dest.SharedWithUserName, opt => opt.MapFrom(src => src.SharedWithUser.UserName));
            CreateMap<NoteShareCreateDto, NoteShare>();
            CreateMap<NoteShareUpdateDto, NoteShare>();
            CreateMap<NoteShareDeleteDto, NoteShare>();

            CreateMap<BookShare, BookShareReadDto>()
                .ForMember(dest => dest.SharedWithUserName, opt => opt.MapFrom(src => src.SharedWithUser.UserName));
            CreateMap<BookShareCreateDto, BookShare>();
            CreateMap<BookShareUpdateDto, BookShare>();
            CreateMap<BookShareDeleteDto, BookShare>();

            CreateMap<Category, CategoryReadDto>()
                .ForMember(dest => dest.BooksCount, opt => opt.MapFrom(src => src.Books.Count));
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryDeleteDto, Category>();

            CreateMap<Order, OrderReadDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));
            CreateMap<OrderCreateDto, Order>();
            CreateMap<OrderUpdateDto, Order>();
            CreateMap<OrderDeleteDto, Order>();

            CreateMap<OrderDetail, OrderDetailReadDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.Book.Title));
            CreateMap<OrderDetailCreateDto, OrderDetail>();
            CreateMap<OrderDetailUpdateDto, OrderDetail>();
            CreateMap<OrderDetailDeleteDto, OrderDetail>();
        }
    }
}
