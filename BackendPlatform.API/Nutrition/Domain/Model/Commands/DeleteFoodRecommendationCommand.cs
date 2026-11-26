using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record DeleteFoodRecommendationCommand(FoodRecommendationId FoodRecommendationId)
{
    public DeleteFoodRecommendationCommand(int foodRecommendationId)
        : this(new FoodRecommendationId(foodRecommendationId))
    {
    }
}
