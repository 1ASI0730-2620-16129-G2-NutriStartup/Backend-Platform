using Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="AppointmentConfirmedEvent" />. Notifies the patient of the confirmation.
/// </summary>
public class AppointmentConfirmedEventHandler(
    IAppointmentNotificationService notificationService,
    ILogger<AppointmentConfirmedEventHandler> logger) : IEventHandler<AppointmentConfirmedEvent>
{
    /// <inheritdoc />
    public async Task Handle(AppointmentConfirmedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Appointment {AppointmentId} confirmed: {Slot}",
            domainEvent.AppointmentId.Value, domainEvent.Slot.Describe());

        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Patient, domainEvent.PatientId.Value,
            ENotificationType.AppointmentConfirmed, domainEvent.Slot.Describe()), cancellationToken);
    }
}
