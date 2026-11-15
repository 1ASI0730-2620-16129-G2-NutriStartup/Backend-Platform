using BackendPlatform.API.Nutrition.Application.QueryServices;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;
using BackendPlatform.API.Nutrition.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.QueryServices;

public class MealPlanQueryService(IMealPlanRepository mealPlanRepository) : IMealPlanQueryService
{
    public async Task<MealPlan?> Handle(GetMealPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await mealPlanRepository.FindByIdAsNoTrackingAsync(query.MealPlanId, cancellationToken);
    }

    public async Task<IEnumerable<MealPlan>> Handle(GetAllMealPlansQuery query, CancellationToken cancellationToken)
    {
        return await mealPlanRepository.ListAsNoTrackingAsync(cancellationToken);
    }

    public async Task<IEnumerable<MealPlan>> Handle(GetAllMealPlansByNutritionPlanIdQuery query, CancellationToken cancellationToken)
    {
        return await mealPlanRepository.FindByNutritionPlanIdAsNoTrackingAsync(query.NutritionPlanId, cancellationToken);
    }
}
