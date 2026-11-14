using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to move an appointment to another availability of the same nutritionist (reprogramar cita).
/// </summary>
/// <param name="AppointmentId">The appointment to reschedule.</param>
/// <param name="NewDate">The day of the new availability.</param>
/// <param name="NewStartTime">The start time of the new availability.</param>
/// <param name="NewEndTime">The end time of the new availability.</param>
public record RescheduleAppointmentCommand(
    AppointmentId AppointmentId,
    DateOnly NewDate,
    TimeOnly NewStartTime,
    TimeOnly NewEndTime)
{
    public RescheduleAppointmentCommand(int appointmentId, DateOnly newDate, TimeOnly newStartTime,
        TimeOnly newEndTime)
        : this(new AppointmentId(appointmentId), newDate, newStartTime, newEndTime)
    {
    }
}
