using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record CreateMealPlanCommand(
    NutritionPlanId NutritionPlanId,
    string MealType,
    string Description,
    int Calories)
{
    public CreateMealPlanCommand(
        int nutritionPlanId,
        string mealType,
        string description,
        int calories)
        : this(
            new NutritionPlanId(nutritionPlanId),
            mealType,
            description,
            calories)
    {
        
    }
}
