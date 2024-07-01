namespace LibraryArchive.Services.DTOs.NoteShare
{
    public class NoteShareReadDto
    {
        public int NoteShareId { get; set; }
        public int NoteId { get; set; }
        public string SharedWithUserId { get; set; }
        public string NoteContent { get; set; }  // Paylaşılan notun içeriği
        public string SharedWithUserName { get; set; }  // Paylaşım yapılan kullanıcının adı
        public string ShareType { get; set; }
    }
}
