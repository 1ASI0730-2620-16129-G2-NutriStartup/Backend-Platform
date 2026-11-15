using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;

namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a <see cref="ProgressEntry" /> into a resource.
/// </summary>
public static class ProgressEntryResourceFromEntityAssembler
{
    /// <summary>
    ///     Converts the entity into a <see cref="ProgressEntryResource" />.
    /// </summary>
    /// <param name="entity">The <see cref="ProgressEntry" /> entity.</param>
    public static ProgressEntryResource ToResourceFromEntity(ProgressEntry entity)
    {
        return new ProgressEntryResource(
            entity.Id,
            entity.UserId,
            entity.RecordedAt,
            entity.Measurements.WeightKg,
            entity.Measurements.BodyFatPercentage,
            entity.Measurements.WaistCm,
            entity.Notes);
    }
}