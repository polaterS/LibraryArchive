﻿namespace LibraryArchive.Services.DTOs.Notification
{
    public class NotificationSettingsUpdateDto
    {
        public int NotificationSettingsId { get; set; }
        public string UserId { get; set; }
        public bool EmailNotificationsEnabled { get; set; }
        public bool SmsNotificationsEnabled { get; set; }
        public bool PushNotificationsEnabled { get; set; }
    }
}