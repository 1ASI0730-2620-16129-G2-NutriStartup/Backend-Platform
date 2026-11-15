using BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;
using BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;

namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts an <see cref="UpdateProgressEntryResource" /> into a command.
/// </summary>
public static class UpdateProgressEntryCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into an <see cref="UpdateProgressEntryCommand" />.
    /// </summary>
    /// <param name="id">The id of the entry to update (comes from the route).</param>
    /// <param name="resource">The <see cref="UpdateProgressEntryResource" /> resource.</param>
    public static UpdateProgressEntryCommand ToCommandFromResource(int id, UpdateProgressEntryResource resource)
    {
        return new UpdateProgressEntryCommand(
            id,
            resource.RecordedAt,
            resource.WeightKg,
            resource.BodyFatPercentage,
            resource.WaistCm,
            resource.Notes);
    }
}