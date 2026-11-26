using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Domain.Repositories;

public interface IFoodRecommendationRepository : IBaseRepository<FoodRecommendation>
{
    Task<FoodRecommendation?> FindByIdAsNoTrackingAsync(FoodRecommendationId id, CancellationToken cancellationToken = default);

    Task<FoodRecommendation?> FindTrackedByIdAsync(
        FoodRecommendationId id,
        CancellationToken cancellationToken = default);
    
    Task<IEnumerable<FoodRecommendation>> ListWithRelatedEntitiesAsNoTrackingAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<FoodRecommendation>> FindByMealPlanIdAsNoTrackingAsync(int mealPlanId, CancellationToken cancellationToken);
}
