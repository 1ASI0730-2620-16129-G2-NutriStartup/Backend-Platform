using Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="AppointmentBookedEvent" />. Notifies the nutritionist of the new booking.
/// </summary>
public class AppointmentBookedEventHandler(
    IAppointmentNotificationService notificationService,
    ILogger<AppointmentBookedEventHandler> logger) : IEventHandler<AppointmentBookedEvent>
{
    /// <inheritdoc />
    public async Task Handle(AppointmentBookedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Appointment booked by patient {PatientId} with nutritionist {NutritionistId}: {Slot}",
            domainEvent.PatientId.Value, domainEvent.NutritionistId.Value, domainEvent.Slot.Describe());

        await notificationService.NotifyAsync(new AppointmentNotification(
            ENotificationRecipient.Nutritionist, domainEvent.NutritionistId.Value,
            ENotificationType.AppointmentBooked, domainEvent.Slot.Describe()), cancellationToken);
    }
}
