using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using BackendPlatform.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendPlatform.API.UserProfileManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PatientProfileRepository(AppDbContext context)
    : BaseRepository<PatientProfile>(context), IPatientProfileRepository
{
    private IQueryable<PatientProfile> IncludeDetails(IQueryable<PatientProfile> query) => query
        .Include(profile => profile.Goals)
        .Include(profile => profile.Preferences)
        .Include(profile => profile.Restrictions)
        .AsSplitQuery();

    public Task<PatientProfile?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>())
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);

    public Task<PatientProfile?> FindByIdWithDetailsAsync(int patientProfileId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>())
            .FirstOrDefaultAsync(profile => profile.Id == patientProfileId, cancellationToken);

    public Task<PatientProfile?> FindByIdWithDetailsAsNoTrackingAsync(int patientProfileId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>().AsNoTracking())
            .FirstOrDefaultAsync(profile => profile.Id == patientProfileId, cancellationToken);

    public Task<PatientProfile?> FindByUserIdWithDetailsAsNoTrackingAsync(string userId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>().AsNoTracking())
            .FirstOrDefaultAsync(profile => profile.UserId == userId, cancellationToken);

    public Task<PatientProfile?> FindByGoalIdAsync(int goalId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>())
            .FirstOrDefaultAsync(profile => profile.Goals.Any(goal => goal.Id == goalId), cancellationToken);

    public Task<PatientProfile?> FindByGoalIdAsNoTrackingAsync(int goalId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>().AsNoTracking())
            .FirstOrDefaultAsync(profile => profile.Goals.Any(goal => goal.Id == goalId), cancellationToken);

    public Task<PatientProfile?> FindByPreferenceIdAsync(int preferenceId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>())
            .FirstOrDefaultAsync(profile => profile.Preferences.Any(preference => preference.Id == preferenceId), cancellationToken);

    public Task<PatientProfile?> FindByPreferenceIdAsNoTrackingAsync(int preferenceId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>().AsNoTracking())
            .FirstOrDefaultAsync(profile => profile.Preferences.Any(preference => preference.Id == preferenceId), cancellationToken);

    public Task<PatientProfile?> FindByRestrictionIdAsync(int restrictionId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>())
            .FirstOrDefaultAsync(profile => profile.Restrictions.Any(restriction => restriction.Id == restrictionId), cancellationToken);

    public Task<PatientProfile?> FindByRestrictionIdAsNoTrackingAsync(int restrictionId, CancellationToken cancellationToken = default) =>
        IncludeDetails(Context.Set<PatientProfile>().AsNoTracking())
            .FirstOrDefaultAsync(profile => profile.Restrictions.Any(restriction => restriction.Id == restrictionId), cancellationToken);
}
