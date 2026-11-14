using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="RescheduleAppointmentResource" /> into a
///     <see cref="RescheduleAppointmentCommand" />.
/// </summary>
public static class RescheduleAppointmentCommandFromResourceAssembler
{
    public static RescheduleAppointmentCommand ToCommandFromResource(RescheduleAppointmentResource resource,
        int appointmentId)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return new RescheduleAppointmentCommand(appointmentId, resource.Date, resource.StartTime,
            resource.EndTime);
    }
}
