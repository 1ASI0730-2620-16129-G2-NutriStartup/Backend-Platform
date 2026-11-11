using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record CreateFoodRecommendationCommand(MealPlanId MealPlanId, string FoodName, string Portion,
    string NutritionalValue)
{
    public CreateFoodRecommendationCommand(
        int mealPlanId,
        string foodName,
        string portion,
        string nutritionalValue)
        : this(
            new MealPlanId(mealPlanId),
            foodName,
            portion,
            nutritionalValue)
    {
        
    }
}
