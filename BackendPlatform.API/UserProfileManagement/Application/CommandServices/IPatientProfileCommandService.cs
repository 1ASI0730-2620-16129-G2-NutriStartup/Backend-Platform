using BackendPlatform.API.Shared.Application.Patterns;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Errors;

namespace BackendPlatform.API.UserProfileManagement.Application.CommandServices;

public interface IPatientProfileCommandService
{
    Task<Result<PatientProfile, UserProfileManagementError>> Handle(CreatePatientProfileCommand command, CancellationToken cancellationToken);
    
    Task<Result<PatientProfile, UserProfileManagementError>> Handle(UpdatePatientProfileCommand command, CancellationToken cancellationToken);
    
    Task<Result<Goal, UserProfileManagementError>> Handle(CreateGoalCommand command, CancellationToken cancellationToken);
    
    Task<Result<Goal, UserProfileManagementError>> Handle(UpdateGoalCommand command, CancellationToken cancellationToken);
    
    Task<Result<bool, UserProfileManagementError>> Handle(DeleteGoalCommand command, CancellationToken cancellationToken);
    
    Task<Result<Preference, UserProfileManagementError>> Handle(CreatePreferenceCommand command, CancellationToken cancellationToken);
    
    Task<Result<Preference, UserProfileManagementError>> Handle(UpdatePreferenceCommand command, CancellationToken cancellationToken);
    
    Task<Result<bool, UserProfileManagementError>> Handle(DeletePreferenceCommand command, CancellationToken cancellationToken);
    
    Task<Result<Restriction, UserProfileManagementError>> Handle(CreateRestrictionCommand command, CancellationToken cancellationToken);
    
    Task<Result<Restriction, UserProfileManagementError>> Handle(UpdateRestrictionCommand command, CancellationToken cancellationToken);
    
    Task<Result<bool, UserProfileManagementError>> Handle(DeleteRestrictionCommand command, CancellationToken cancellationToken);
}
