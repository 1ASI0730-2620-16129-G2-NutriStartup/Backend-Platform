using BackendPlatform.API.Shared.Domain.Repositories;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

namespace BackendPlatform.API.UserProfileManagement.Domain.Repositories;

public interface IPatientProfileRepository : IBaseRepository<PatientProfile>
{
    Task<PatientProfile?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByIdWithDetailsAsync(int patientProfileId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByIdWithDetailsAsNoTrackingAsync(int patientProfileId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByUserIdWithDetailsAsNoTrackingAsync(string userId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByGoalIdAsync(int goalId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByGoalIdAsNoTrackingAsync(int goalId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByPreferenceIdAsync(int preferenceId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByPreferenceIdAsNoTrackingAsync(int preferenceId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByRestrictionIdAsync(int restrictionId, CancellationToken cancellationToken = default);
    
    Task<PatientProfile?> FindByRestrictionIdAsNoTrackingAsync(int restrictionId, CancellationToken cancellationToken = default);
}
