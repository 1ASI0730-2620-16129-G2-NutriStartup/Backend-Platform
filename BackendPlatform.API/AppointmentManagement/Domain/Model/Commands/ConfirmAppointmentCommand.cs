using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to confirm an appointment (confirmar cita).
/// </summary>
/// <param name="AppointmentId">The appointment to confirm.</param>
public record ConfirmAppointmentCommand(AppointmentId AppointmentId)
{
    public ConfirmAppointmentCommand(int appointmentId) : this(new AppointmentId(appointmentId))
    {
    }
}
