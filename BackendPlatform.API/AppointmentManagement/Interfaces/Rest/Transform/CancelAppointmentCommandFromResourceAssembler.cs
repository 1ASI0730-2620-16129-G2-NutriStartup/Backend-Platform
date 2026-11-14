using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="CancelAppointmentResource" /> (optional) into a
///     <see cref="CancelAppointmentCommand" />.
/// </summary>
public static class CancelAppointmentCommandFromResourceAssembler
{
    public static CancelAppointmentCommand ToCommandFromResource(CancelAppointmentResource? resource,
        int appointmentId)
    {
        return new CancelAppointmentCommand(appointmentId, resource?.Reason);
    }
}
