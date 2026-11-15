using BackendPlatform.API.ProgressMonitoring.Domain.Model.Aggregates;
using BackendPlatform.API.ProgressMonitoring.Domain.Repositories;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.ProgressMonitoring.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
///     Entity Framework implementation of <see cref="IProgressEntryRepository" />.
/// </summary>
/// <param name="context">The <see cref="AppDbContext" /> database context.</param>
public class ProgressEntryRepository(AppDbContext context)
    : BaseRepository<ProgressEntry>(context), IProgressEntryRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<ProgressEntry>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<ProgressEntry>()
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.RecordedAt)
            .ToListAsync();
    }
}