namespace LibraryArchive.Services.DTOs.Note
{
    public class NoteReadDto
    {
        public int NoteId { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
        public bool IsPrivate { get; set; }
        public string UserName { get; set; }  // Notu oluşturan kullanıcı adı
        public string BookTitle { get; set; }  // Notun ait olduğu kitabın başlığı
    }
}
