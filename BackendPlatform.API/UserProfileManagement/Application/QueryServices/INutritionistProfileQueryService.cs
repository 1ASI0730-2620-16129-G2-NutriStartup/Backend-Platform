using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;

namespace BackendPlatform.API.UserProfileManagement.Application.QueryServices;

public interface INutritionistProfileQueryService
{
    Task<NutritionistProfile?> Handle(GetNutritionistProfileByIdQuery query, CancellationToken cancellationToken);
    
    Task<NutritionistProfile?> Handle(GetNutritionistProfileByUserIdQuery query, CancellationToken cancellationToken);
}
