namespace LibraryArchive.Data.Entities
{
    public class NotificationSettings
    {
        public int NotificationSettingsId { get; set; }
        public string UserId { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
        public bool SmsNotificationsEnabled { get; set; }
        public bool PushNotificationsEnabled { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
