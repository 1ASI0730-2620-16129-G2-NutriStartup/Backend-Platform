namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.OutboundServices;

/// <summary>
///     Kind of notification emitted by the Appointment Management bounded context.
/// </summary>
public enum ENotificationType
{
    AppointmentBooked,
    AppointmentConfirmed,
    AppointmentRescheduled,
    AppointmentCancelled,
    FollowUpScheduled
}
