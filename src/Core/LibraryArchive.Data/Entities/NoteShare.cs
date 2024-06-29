namespace LibraryArchive.Data.Entities
{
    public class NoteShare
    {
        public int NoteShareId { get; set; }
        public int NoteId { get; set; }
        public string SharedWithUserId { get; set; }
        public string ShareType { get; set; }
        public virtual Note Note { get; set; }
        public virtual ApplicationUser SharedWithUser { get; set; }
    }
}

