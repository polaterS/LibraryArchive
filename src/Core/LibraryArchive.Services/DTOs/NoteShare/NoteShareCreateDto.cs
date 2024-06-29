namespace LibraryArchive.Services.DTOs.NoteShare
{
    public class NoteShareCreateDto
    {
        public int NoteId { get; set; }
        public string SharedWithUserId { get; set; }
        public string ShareType { get; set; }
    }
}
