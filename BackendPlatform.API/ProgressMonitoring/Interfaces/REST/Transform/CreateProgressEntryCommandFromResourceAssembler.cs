using BackendPlatform.API.ProgressMonitoring.Domain.Model.Commands;
using BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;

namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a <see cref="CreateProgressEntryResource" /> into a command.
/// </summary>
public static class CreateProgressEntryCommandFromResourceAssembler
{
    /// <summary>
    ///     Converts the resource into a <see cref="CreateProgressEntryCommand" />.
    /// </summary>
    /// <param name="resource">The <see cref="CreateProgressEntryResource" /> resource.</param>
    public static CreateProgressEntryCommand ToCommandFromResource(CreateProgressEntryResource resource)
    {
        return new CreateProgressEntryCommand(
            resource.UserId,
            resource.RecordedAt,
            resource.WeightKg,
            resource.BodyFatPercentage,
            resource.WaistCm,
            resource.Notes);
    }
}