using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.Queries;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.ValueObjects;
using BackendPlatform.API.ProgressMonitoring.Domain.Repositories;
using BackendPlatform.API.ProgressMonitoring.Domain.Services;

namespace BackendPlatform.API.ProgressMonitoring.Application.Internal.QueryServices;

/// <summary>
///     Query service implementation for <see cref="ProgressEntry" />.
/// </summary>
/// <param name="progressEntryRepository">The <see cref="IProgressEntryRepository" /> repository.</param>
public class ProgressEntryQueryService(IProgressEntryRepository progressEntryRepository) : IProgressEntryQueryService
{
    public async Task<ProgressEntry?> Handle(GetProgressEntryByIdQuery query)
    {
        return await progressEntryRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<ProgressEntry>> Handle(GetAllProgressEntriesByUserIdQuery query)
    {
        return await progressEntryRepository.FindByUserIdAsync(query.UserId);
    }

    public async Task<ProgressSummary?> Handle(GetProgressSummaryByUserIdQuery query)
    {

        var entries = (await progressEntryRepository.FindByUserIdAsync(query.UserId)).ToList();
        if (entries.Count == 0) return null;

        var first = entries[0];
        var last = entries[^1];
        var weights = entries.Select(e => e.Measurements.WeightKg).ToList();

        return new ProgressSummary(
            query.UserId,
            entries.Count,
            first.Measurements.WeightKg,
            last.Measurements.WeightKg,
            last.Measurements.WeightKg - first.Measurements.WeightKg,
            weights.Min(),
            weights.Max(),
            first.RecordedAt,
            last.RecordedAt);
    }
}