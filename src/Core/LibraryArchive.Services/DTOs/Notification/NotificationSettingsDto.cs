namespace LibraryArchive.Services.DTOs.Notification
{
    public class NotificationSettingsDto
    {
        public int NotificationSettingsId { get; set; }
        public string UserId { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
        public bool SmsNotificationsEnabled { get; set; }
        public bool PushNotificationsEnabled { get; set; }
    }
}
