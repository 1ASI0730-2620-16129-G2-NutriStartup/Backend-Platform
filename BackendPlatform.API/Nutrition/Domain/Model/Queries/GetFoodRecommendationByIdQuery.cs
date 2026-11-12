using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Queries;

public record GetFoodRecommendationByIdQuery(FoodRecommendationId FoodRecommendationId)
{
    public GetFoodRecommendationByIdQuery(int foodRecommendationId)
        : this(new FoodRecommendationId(foodRecommendationId))
    {
    }
}
