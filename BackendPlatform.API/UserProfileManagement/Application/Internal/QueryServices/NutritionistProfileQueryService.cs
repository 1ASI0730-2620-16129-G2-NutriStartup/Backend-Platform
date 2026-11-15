using BackendPlatform.API.UserProfileManagement.Application.QueryServices;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Queries;
using BackendPlatform.API.UserProfileManagement.Domain.Repositories;

namespace BackendPlatform.API.UserProfileManagement.Application.Internal.QueryServices;

public class NutritionistProfileQueryService(INutritionistProfileRepository nutritionistProfileRepository)
    : INutritionistProfileQueryService
{
    public Task<NutritionistProfile?> Handle(GetNutritionistProfileByIdQuery query, CancellationToken cancellationToken) =>
        nutritionistProfileRepository.FindByIdAsNoTrackingAsync(query.NutritionistProfileId, cancellationToken);

    public Task<NutritionistProfile?> Handle(GetNutritionistProfileByUserIdQuery query, CancellationToken cancellationToken) =>
        nutritionistProfileRepository.FindByUserIdAsNoTrackingAsync(query.UserId, cancellationToken);
}
