namespace LibraryArchive.Services.DTOs.BookShare
{
    public class BookShareCreateDto
    {
        public int NoteId { get; set; }
        public string SharedWithUserId { get; set; }
        public string ShareType { get; set; }
    }
}
