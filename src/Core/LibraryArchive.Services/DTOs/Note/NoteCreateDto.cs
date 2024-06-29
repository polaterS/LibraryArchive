namespace LibraryArchive.Services.DTOs.Note
{
    public class NoteCreateDto
    {
        public int BookId { get; set; }
        public string Content { get; set; }
        public bool IsPrivate { get; set; }
    }
}
