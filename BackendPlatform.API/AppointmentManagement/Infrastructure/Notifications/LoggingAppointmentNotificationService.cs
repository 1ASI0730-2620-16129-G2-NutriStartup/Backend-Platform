using Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Notifications;

/// <summary>
///     Default adapter of <see cref="IAppointmentNotificationService" /> that writes the notifications to the
///     application log. Replace it with an e-mail or push adapter once the channel is decided.
/// </summary>
public class LoggingAppointmentNotificationService(ILogger<LoggingAppointmentNotificationService> logger)
    : IAppointmentNotificationService
{
    /// <inheritdoc />
    public Task NotifyAsync(AppointmentNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Notification {Type} sent to {Recipient} {RecipientId}: {Details}",
            notification.Type, notification.Recipient, notification.RecipientId, notification.Details);
        return Task.CompletedTask;
    }
}
