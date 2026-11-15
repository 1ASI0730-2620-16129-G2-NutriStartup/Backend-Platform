using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;

namespace BackendPlatform.API.UserProfileManagement.Application.QueryServices;

public interface IPatientProfileQueryService
{
    Task<PatientProfile?> Handle(GetPatientProfileByIdQuery query, CancellationToken cancellationToken);
    
    Task<PatientProfile?> Handle(GetPatientProfileByUserIdQuery query, CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<Goal>> Handle(GetGoalsByUserProfileIdQuery query, CancellationToken cancellationToken);
    
    Task<Goal?> Handle(GetGoalByIdQuery query, CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<Preference>> Handle(GetPreferencesByUserIdQuery query, CancellationToken cancellationToken);
    
    Task<Preference?> Handle(GetPreferenceByIdQuery query, CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<Restriction>> Handle(GetRestrictionsByUserIdQuery query, CancellationToken cancellationToken);
    
    Task<Restriction?> Handle(GetRestrictionByIdQuery query, CancellationToken cancellationToken);
}
