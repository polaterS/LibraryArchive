using Microsoft.AspNetCore.Identity;

namespace LibraryArchive.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool IsActive { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<NotificationSettings> NotificationSettings { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
