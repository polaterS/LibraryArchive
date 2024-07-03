using FluentValidation;
using LibraryArchive.Services.DTOs.Notification;

namespace LibraryArchive.Services.Validation.Notification
{
    public class NotificationReadDtoValidator : AbstractValidator<NotificationReadDto>
    {
        public NotificationReadDtoValidator()
        {
            RuleFor(x => x.NotificationId)
                .GreaterThan(0).WithMessage("Bildirim ID'si 0'dan büyük olmalıdır.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si gereklidir.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık gereklidir.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj gereklidir.");

            RuleFor(x => x.Date)
                .GreaterThan(DateTime.MinValue).WithMessage("Geçerli bir tarih gereklidir.");

            RuleFor(x => x.NotificationType)
                .NotEmpty().WithMessage("Bildirim türü gereklidir.")
                .Must(BeValidNotificationType).WithMessage("Geçersiz bildirim türü.");
        }

        private bool BeValidNotificationType(string notificationType)
        {
            var validNotificationTypes = new List<string> { "Email", "SMS", "PushNotification" };
            return validNotificationTypes.Contains(notificationType);
        }
    }
}
