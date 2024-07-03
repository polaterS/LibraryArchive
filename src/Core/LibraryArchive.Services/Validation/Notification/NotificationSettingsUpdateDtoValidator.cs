using FluentValidation;
using LibraryArchive.Services.DTOs.Notification;

namespace LibraryArchive.Services.Validation.Notification
{
    public class NotificationSettingsUpdateDtoValidator : AbstractValidator<NotificationSettingsUpdateDto>
    {
        public NotificationSettingsUpdateDtoValidator()
        {
            RuleFor(x => x.NotificationSettingsId)
                .GreaterThan(0).WithMessage("Bildirim Ayarları ID'si 0'dan büyük olmalıdır.");

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
