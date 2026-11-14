namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

/// <summary>
///     Represents the lifecycle status of an appointment (estado de cita).
/// </summary>
/// <remarks>
///     Allowed transitions:
///     <list type="bullet">
///         <item><description>Pending → Confirmed | Rescheduled | Cancelled | Completed</description></item>
///         <item><description>Confirmed → Rescheduled | Cancelled | Completed</description></item>
///         <item><description>Rescheduled → Confirmed | Rescheduled | Cancelled | Completed</description></item>
///         <item><description>Cancelled and Completed are terminal states.</description></item>
///     </list>
/// </remarks>
public enum EAppointmentStatus
{
    Pending,
    Confirmed,
    Rescheduled,
    Cancelled,
    Completed
}
