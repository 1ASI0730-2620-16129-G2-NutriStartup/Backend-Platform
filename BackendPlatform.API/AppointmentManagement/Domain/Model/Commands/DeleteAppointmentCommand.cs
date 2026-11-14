using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to remove a cancelled appointment from the agenda.
/// </summary>
/// <param name="AppointmentId">The appointment to delete.</param>
public record DeleteAppointmentCommand(AppointmentId AppointmentId)
{
    public DeleteAppointmentCommand(int appointmentId) : this(new AppointmentId(appointmentId))
    {
    }
}
