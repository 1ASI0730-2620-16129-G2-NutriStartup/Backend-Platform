using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Domain.Repositories;

public interface IMealPlanRepository : IBaseRepository<MealPlan>
{
    Task<MealPlan?> FindByIdAsNoTrackingAsync(MealPlanId id, CancellationToken cancellationToken = default);
    
    Task<MealPlan?> FindTrackedByIdAsync(
        MealPlanId id,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<MealPlan>> ListWithNutritionPlanAsNoTrackingAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<MealPlan>> FindByNutritionPlanIdAsNoTrackingAsync(int nutritionPlanId, CancellationToken cancellationToken);
}
