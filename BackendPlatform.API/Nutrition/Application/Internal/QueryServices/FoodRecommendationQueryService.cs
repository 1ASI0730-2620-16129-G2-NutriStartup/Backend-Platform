using BackendPlatform.API.Nutrition.Application.QueryServices;
using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;
using BackendPlatform.API.Nutrition.Domain.Repositories;

namespace BackendPlatform.API.Nutrition.Application.Internal.QueryServices;

public class FoodRecommendationQueryService(IFoodRecommendationRepository foodRecommendationRepository)
    : IFoodRecommendationQueryService
{
    public async Task<FoodRecommendation?> Handle(GetFoodRecommendationByIdQuery query, CancellationToken cancellationToken)
    {
        return await foodRecommendationRepository.FindByIdAsNoTrackingAsync(query.FoodRecommendationId, cancellationToken);
    }

    public async Task<IEnumerable<FoodRecommendation>> Handle( GetAllFoodRecommendationsQuery query, CancellationToken cancellationToken)
    {
        return await foodRecommendationRepository.ListWithRelatedEntitiesAsNoTrackingAsync(cancellationToken);
    }

    public async Task<IEnumerable<FoodRecommendation>> Handle(GetAllFoodRecommendationsByMealPlanIdQuery query, CancellationToken cancellationToken)
    {
        return await foodRecommendationRepository.FindByMealPlanIdAsNoTrackingAsync(query.MealPlanId, cancellationToken);
    }
}
