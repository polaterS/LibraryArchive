namespace LibraryArchive.Services.DTOs.Notification
{
    public class NotificationUpdateDto
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string NotificationType { get; set; } // Email, SMS, PushNotification gibi
    }
}
