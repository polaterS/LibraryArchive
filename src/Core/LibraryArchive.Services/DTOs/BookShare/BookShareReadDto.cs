namespace LibraryArchive.Services.DTOs.BookShare
{
    public class BookShareReadDto
    {
        public int BookShareId { get; set; }
        public string NoteTitle { get; set; }  // Note'un başlığı
        public string SharedWithUserName { get; set; }  // Paylaşım yapılan kullanıcının adı
        public string ShareType { get; set; }
    }
}
