using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.ProgressMonitoring.Domain.Repositories;

/// <summary>
///     Repository contract for <see cref="ProgressEntry" />.
/// </summary>
public interface IProgressEntryRepository : IBaseRepository<ProgressEntry>
{
    /// <summary>
    ///     Finds every entry of a user ordered by recorded date (oldest first).
    /// </summary>
    /// <param name="userId">The user id.</param>
    Task<IEnumerable<ProgressEntry>> FindByUserIdAsync(int userId);
}