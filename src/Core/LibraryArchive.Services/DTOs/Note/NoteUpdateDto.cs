namespace LibraryArchive.Services.DTOs.Note
{
    public class NoteUpdateDto
    {
        public int NoteId { get; set; }
        public string Content { get; set; }
        public bool IsPrivate { get; set; }
    }
}
