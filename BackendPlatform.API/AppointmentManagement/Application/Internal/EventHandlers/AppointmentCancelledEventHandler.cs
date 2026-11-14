using Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="AppointmentCancelledEvent" />.
///     Business rule: cancellations are notified to both the patient and the nutritionist.
/// </summary>
public class AppointmentCancelledEventHandler(
    IAppointmentNotificationService notificationService,
    ILogger<AppointmentCancelledEventHandler> logger) : IEventHandler<AppointmentCancelledEvent>
{
    /// <inheritdoc />
    public async Task Handle(AppointmentCancelledEvent domainEvent, CancellationToken cancellationToken)
    {
        var details = string.IsNullOrWhiteSpace(domainEvent.Reason)
            ? domainEvent.Slot.Describe()
            : $"{domainEvent.Slot.Describe()} ({domainEvent.Reason})";
        logger.LogInformation("Appointment {AppointmentId} cancelled: {Details}",
            domainEvent.AppointmentId.Value, details);

        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Patient, domainEvent.PatientId.Value,
            ENotificationType.AppointmentCancelled, details), cancellationToken);
        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Nutritionist, domainEvent.NutritionistId.Value,
            ENotificationType.AppointmentCancelled, details), cancellationToken);
    }
}
