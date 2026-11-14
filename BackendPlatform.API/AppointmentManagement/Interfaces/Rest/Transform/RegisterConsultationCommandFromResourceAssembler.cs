using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="RegisterConsultationResource" /> into a
///     <see cref="RegisterConsultationCommand" />.
/// </summary>
public static class RegisterConsultationCommandFromResourceAssembler
{
    public static RegisterConsultationCommand ToCommandFromResource(RegisterConsultationResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return new RegisterConsultationCommand(resource.AppointmentId, resource.Notes, resource.Recommendations,
            resource.Date, resource.FollowUpDate);
    }
}
