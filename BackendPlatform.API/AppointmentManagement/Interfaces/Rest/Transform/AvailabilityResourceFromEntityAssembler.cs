using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Transform;

/// <summary>
///     Assembler responsible for transforming an <see cref="Availability" /> aggregate into an
///     <see cref="AvailabilityResource" />.
/// </summary>
public static class AvailabilityResourceFromEntityAssembler
{
    public static AvailabilityResource ToResourceFromEntity(Availability entity)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return new AvailabilityResource(entity.Id, entity.NutritionistId, entity.Date, entity.StartTime,
            entity.EndTime, entity.IsAvailable);
    }
}
