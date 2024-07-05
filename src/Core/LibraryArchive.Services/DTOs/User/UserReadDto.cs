using LibraryArchive.Services.DTOs.Address;
using LibraryArchive.Services.DTOs.Book;
using LibraryArchive.Services.DTOs.Note;
using LibraryArchive.Services.DTOs.Order;

namespace LibraryArchive.Services.DTOs.User
{
    public class UserReadDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }
        public string ProfilePictureUrl { get; set; }
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public IEnumerable<BookReadDto> Books { get; set; } = new List<BookReadDto>();
        public IEnumerable<NoteReadDto> Notes { get; set; } = new List<NoteReadDto>();
        public IEnumerable<OrderReadDto> Orders { get; set; } = new List<OrderReadDto>();
        public IEnumerable<AddressReadDto> Addresses { get; set; } = new List<AddressReadDto>();
    }
}
