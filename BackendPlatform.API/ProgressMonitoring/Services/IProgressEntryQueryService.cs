using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.Queries;
using BackendPlatform.API.ProgressMonitoring.Domain.Model.ValueObjects;

namespace BackendPlatform.API.ProgressMonitoring.Domain.Services;

/// <summary>
///     Query service contract for <see cref="ProgressEntry" />.
/// </summary>
public interface IProgressEntryQueryService
{
    /// <summary>
    ///     Handles getting a progress entry by id.
    /// </summary>
    /// <param name="query">The <see cref="GetProgressEntryByIdQuery" /> query.</param>
    /// <returns>The entry, or null if it does not exist.</returns>
    Task<ProgressEntry?> Handle(GetProgressEntryByIdQuery query);

    /// <summary>
    ///     Handles getting all the progress entries of a user.
    /// </summary>
    /// <param name="query">The <see cref="GetAllProgressEntriesByUserIdQuery" /> query.</param>
    /// <returns>The entries ordered by recorded date (oldest first).</returns>
    Task<IEnumerable<ProgressEntry>> Handle(GetAllProgressEntriesByUserIdQuery query);

    /// <summary>
    ///     Handles getting the overall progress summary of a user.
    /// </summary>
    /// <param name="query">The <see cref="GetProgressSummaryByUserIdQuery" /> query.</param>
    /// <returns>The summary, or null if the user has no entries.</returns>
    Task<ProgressSummary?> Handle(GetProgressSummaryByUserIdQuery query);
}