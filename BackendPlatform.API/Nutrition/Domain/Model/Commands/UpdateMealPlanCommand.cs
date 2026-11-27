using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record UpdateMealPlanCommand(
    MealPlanId MealPlanId,
    NutritionPlanId NutritionPlanId,
    string MealType,
    string Description,
    int Calories)
{
    public UpdateMealPlanCommand(
        int mealPlanId,
        int nutritionPlanId,
        string mealType,
        string description,
        int calories)
        : this(
            new MealPlanId(mealPlanId),
            new NutritionPlanId(nutritionPlanId),
            mealType,
            description,
            calories)
    {
    }
}
