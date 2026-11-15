using BackendPlatform.API.Shared.Application.Patterns;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Errors;

namespace BackendPlatform.API.UserProfileManagement.Application.CommandServices;

public interface INutritionistProfileCommandService
{
    Task<Result<NutritionistProfile, UserProfileManagementError>> Handle(CreateNutritionistProfileCommand command, CancellationToken cancellationToken);
    
    Task<Result<NutritionistProfile, UserProfileManagementError>> Handle(UpdateNutritionistProfileCommand command, CancellationToken cancellationToken);
}
