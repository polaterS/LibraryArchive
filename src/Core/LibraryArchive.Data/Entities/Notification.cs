﻿using System;

namespace LibraryArchive.Data.Entities
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string NotificationType { get; set; } // Email, SMS, PushNotification gibi

        public virtual ApplicationUser User { get; set; }
        public int? BookId { get; set; }
        public virtual Book Book { get; set; }
        public int? NoteId { get; set; }
        public virtual Note Note { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}