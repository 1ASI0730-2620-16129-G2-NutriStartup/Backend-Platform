using Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="AppointmentRescheduledEvent" />.
///     Business rule: reschedules are notified to both the patient and the nutritionist.
/// </summary>
public class AppointmentRescheduledEventHandler(
    IAppointmentNotificationService notificationService,
    ILogger<AppointmentRescheduledEventHandler> logger) : IEventHandler<AppointmentRescheduledEvent>
{
    /// <inheritdoc />
    public async Task Handle(AppointmentRescheduledEvent domainEvent, CancellationToken cancellationToken)
    {
        var details = $"{domainEvent.PreviousSlot.Describe()} -> {domainEvent.NewSlot.Describe()}";
        logger.LogInformation("Appointment {AppointmentId} rescheduled: {Details}",
            domainEvent.AppointmentId.Value, details);

        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Patient, domainEvent.PatientId.Value,
            ENotificationType.AppointmentRescheduled, details), cancellationToken);
        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Nutritionist, domainEvent.NutritionistId.Value,
            ENotificationType.AppointmentRescheduled, details), cancellationToken);
    }
}
