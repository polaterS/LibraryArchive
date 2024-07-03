using FluentValidation;
using LibraryArchive.Services.DTOs.Notification;

namespace LibraryArchive.Services.Validation.Notification
{
    public class NotificationSettingsCreateDtoValidator : AbstractValidator<NotificationSettingsCreateDto>
    {
        public NotificationSettingsCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("Kullanıcı ID'si gereklidir.");

            RuleFor(x => x.EmailNotificationsEnabled)
                .NotNull().WithMessage("E-posta bildirim ayarı gereklidir.");

            RuleFor(x => x.SmsNotificationsEnabled)
                .NotNull().WithMessage("SMS bildirim ayarı gereklidir.");

            RuleFor(x => x.PushNotificationsEnabled)
                .NotNull().WithMessage("Push bildirim ayarı gereklidir.");
        }
    }
}
