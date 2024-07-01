namespace LibraryArchive.Services.DTOs.BookShare
{
    public class BookShareReadDto
    {
        public int BookShareId { get; set; }
        public int NoteId { get; set; }
        public string SharedWithUserId { get; set; }
        public string NoteTitle { get; set; }
        public string SharedWithUserName { get; set; }
        public string ShareType { get; set; }
    }
}
