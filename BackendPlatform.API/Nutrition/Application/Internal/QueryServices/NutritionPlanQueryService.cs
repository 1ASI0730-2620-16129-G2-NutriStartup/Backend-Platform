using BackendPlatform.API.Nutrition.Application.QueryServices;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;
using BackendPlatform.API.Nutrition.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.QueryServices;

public class NutritionPlanQueryService(INutritionPlanRepository nutritionPlanRepository)
    : INutritionPlanQueryService
{
    public async Task<NutritionPlan?> Handle(GetNutritionPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await nutritionPlanRepository.FindByIdAsNoTrackingAsync(query.NutritionPlanId, cancellationToken);
    }

    public async Task<IEnumerable<NutritionPlan>> Handle(GetAllNutritionPlansQuery query, CancellationToken cancellationToken)
    {
        return await nutritionPlanRepository.ListAsNoTrackingAsync(cancellationToken);
    }
}
