namespace LibraryArchive.Data.Entities
{
    public class Note
    {
        public string UserId { get; set; }
        public int NoteId { get; set; }
        public int BookId { get; set; }
        public string Content { get; set; }
        public bool IsPrivate { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Book Book { get; set; }
        public virtual ICollection<NoteShare> NoteShares { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}