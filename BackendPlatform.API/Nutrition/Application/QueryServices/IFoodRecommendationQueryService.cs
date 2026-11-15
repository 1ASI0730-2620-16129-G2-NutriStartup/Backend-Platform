using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Domain.Model.Queries;

namespace BackendPlatform.API.Nutrition.Application.QueryServices;

public interface IFoodRecommendationQueryService
{
    Task<FoodRecommendation?> Handle(GetFoodRecommendationByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<FoodRecommendation>> Handle(GetAllFoodRecommendationsQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<FoodRecommendation>> Handle(GetAllFoodRecommendationsByMealPlanIdQuery query, CancellationToken cancellationToken);
}
