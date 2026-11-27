using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record UpdateFoodRecommendationCommand(
    FoodRecommendationId FoodRecommendationId,
    MealPlanId MealPlanId,
    string FoodName,
    string Portion,
    string NutritionalValue)
{
    public UpdateFoodRecommendationCommand(
        int foodRecommendationId,
        int mealPlanId,
        string foodName,
        string portion,
        string nutritionalValue)
        : this(
            new FoodRecommendationId(foodRecommendationId),
            new MealPlanId(mealPlanId),
            foodName,
            portion,
            nutritionalValue)
    {
    }
}
