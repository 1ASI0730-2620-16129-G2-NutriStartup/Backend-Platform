using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Domain.Repositories;

public interface INutritionPlanRepository : IBaseRepository<NutritionPlan>
{
    Task<NutritionPlan?> FindByIdAsNoTrackingAsync(
        NutritionPlanId id,
        CancellationToken cancellationToken = default);
}
