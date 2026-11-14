using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming a <see cref="PublishAvailabilityResource" /> into a
///     <see cref="PublishAvailabilityCommand" />.
/// </summary>
public static class PublishAvailabilityCommandFromResourceAssembler
{
    public static PublishAvailabilityCommand ToCommandFromResource(PublishAvailabilityResource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);
        return new PublishAvailabilityCommand(resource.NutritionistId, resource.Date, resource.StartTime,
            resource.EndTime);
    }
}
