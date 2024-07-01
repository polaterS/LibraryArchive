namespace LibraryArchive.Services.DTOs.BookShare
{
    public class BookShareUpdateDto
    {
        public int BookShareId { get; set; }
        public string ShareType { get; set; }
        public int NoteId { get; set; } 
        public string SharedWithUserId { get; set; }
    }
}
