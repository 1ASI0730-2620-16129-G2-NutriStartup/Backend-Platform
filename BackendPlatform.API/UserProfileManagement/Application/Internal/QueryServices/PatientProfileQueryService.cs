using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Domain.Repositories;

namespace BackendPlatform.API.UserProfileManagement.Application.Internal.QueryServices;

public class PatientProfileQueryService(IPatientProfileRepository patientProfileRepository) : IPatientProfileQueryService
{
    public Task<PatientProfile?> Handle(GetPatientProfileByIdQuery query, CancellationToken cancellationToken) =>
        patientProfileRepository.FindByIdWithDetailsAsNoTrackingAsync(query.PatientProfileId, cancellationToken);

    public Task<PatientProfile?> Handle(GetPatientProfileByUserIdQuery query, CancellationToken cancellationToken) =>
        patientProfileRepository.FindByUserIdWithDetailsAsNoTrackingAsync(query.UserId, cancellationToken);

    public async Task<IReadOnlyCollection<Goal>> Handle(GetGoalsByUserProfileIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByIdWithDetailsAsNoTrackingAsync(query.UserProfileId, cancellationToken);
        return profile?.Goals.ToArray() ?? Array.Empty<Goal>();
    }

    public async Task<Goal?> Handle(GetGoalByIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByGoalIdAsNoTrackingAsync(query.GoalId, cancellationToken);
        return profile?.Goals.FirstOrDefault(item => item.Id == query.GoalId);
    }

    public async Task<IReadOnlyCollection<Preference>> Handle(GetPreferencesByUserIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByUserIdWithDetailsAsNoTrackingAsync(query.UserId, cancellationToken);
        return profile?.Preferences.ToArray() ?? Array.Empty<Preference>();
    }

    public async Task<Preference?> Handle(GetPreferenceByIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByPreferenceIdAsNoTrackingAsync(query.PreferenceId, cancellationToken);
        return profile?.Preferences.FirstOrDefault(item => item.Id == query.PreferenceId);
    }

    public async Task<IReadOnlyCollection<Restriction>> Handle(GetRestrictionsByUserIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByUserIdWithDetailsAsNoTrackingAsync(query.UserId, cancellationToken);
        return profile?.Restrictions.ToArray() ?? Array.Empty<Restriction>();
    }

    public async Task<Restriction?> Handle(GetRestrictionByIdQuery query, CancellationToken cancellationToken)
    {
        var profile = await patientProfileRepository.FindByRestrictionIdAsNoTrackingAsync(query.RestrictionId, cancellationToken);
        return profile?.Restrictions.FirstOrDefault(item => item.Id == query.RestrictionId);
    }
}
