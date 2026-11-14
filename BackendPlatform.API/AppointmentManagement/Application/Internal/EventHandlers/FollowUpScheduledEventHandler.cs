using Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="FollowUpScheduledEvent" />. Notifies the patient of the follow-up.
/// </summary>
public class FollowUpScheduledEventHandler(
    IAppointmentNotificationService notificationService,
    ILogger<FollowUpScheduledEventHandler> logger) : IEventHandler<FollowUpScheduledEvent>
{
    /// <inheritdoc />
    public async Task Handle(FollowUpScheduledEvent domainEvent, CancellationToken cancellationToken)
    {
        var details = domainEvent.FollowUpDate.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        logger.LogInformation("Follow-up scheduled for appointment {AppointmentId} on {Details}",
            domainEvent.AppointmentId.Value, details);

        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Patient, domainEvent.PatientId.Value,
            ENotificationType.FollowUpScheduled, details), cancellationToken);
    }
}
