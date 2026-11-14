using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to get an appointment by its id.
/// </summary>
/// <param name="AppointmentId">The id of the appointment.</param>
public record GetAppointmentByIdQuery(AppointmentId AppointmentId)
{
    public GetAppointmentByIdQuery(int appointmentId) : this(new AppointmentId(appointmentId))
    {
    }
}
