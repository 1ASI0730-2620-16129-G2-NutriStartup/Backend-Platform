using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to cancel an appointment (cancelar cita).
/// </summary>
/// <param name="AppointmentId">The appointment to cancel.</param>
/// <param name="Reason">The optional reason of the cancellation.</param>
public record CancelAppointmentCommand(AppointmentId AppointmentId, string? Reason)
{
    public CancelAppointmentCommand(int appointmentId, string? reason)
        : this(new AppointmentId(appointmentId), reason)
    {
    }
}
