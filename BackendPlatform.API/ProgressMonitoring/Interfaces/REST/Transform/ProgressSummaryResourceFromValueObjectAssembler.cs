using BackendPlatform.API.ProgressMonitoring.Domain.Model.ValueObjects;
using BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Resources;

namespace BackendPlatform.API.ProgressMonitoring.Interfaces.REST.Transform;

/// <summary>
///     Assembler that converts a <see cref="ProgressSummary" /> into a resource.
/// </summary>
public static class ProgressSummaryResourceFromValueObjectAssembler
{
    /// <summary>
    ///     Converts the value object into a <see cref="ProgressSummaryResource" />.
    /// </summary>
    /// <param name="summary">The <see cref="ProgressSummary" /> value object.</param>
    public static ProgressSummaryResource ToResourceFromValueObject(ProgressSummary summary)
    {
        return new ProgressSummaryResource(
            summary.UserId,
            summary.TotalEntries,
            summary.StartWeightKg,
            summary.CurrentWeightKg,
            summary.WeightChangeKg,
            summary.LowestWeightKg,
            summary.HighestWeightKg,
            summary.FirstRecordedAt,
            summary.LastRecordedAt);
    }
}