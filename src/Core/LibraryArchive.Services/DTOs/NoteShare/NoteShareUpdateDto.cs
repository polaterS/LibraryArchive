namespace LibraryArchive.Services.DTOs.NoteShare
{
    public class NoteShareUpdateDto
    {
        public int NoteShareId { get; set; }
        public string ShareType { get; set; }
        public int NoteId { get; set; }  // Notun ID'si
        public string SharedWithUserId { get; set; }  // Paylaşılan kullanıcının ID'si
    }
}
