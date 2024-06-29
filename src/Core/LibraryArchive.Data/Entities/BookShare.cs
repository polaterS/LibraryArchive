namespace LibraryArchive.Data.Entities
{
    public class BookShare
    {
        public int BookShareId { get; set; }
        public int NoteId { get; set; }
        public string SharedWithUserId { get; set; }
        public string ShareType { get; set; }
        public virtual Note Note { get; set; }
        public virtual ApplicationUser SharedWithUser { get; set; }
    }
}
