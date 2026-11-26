using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record DeleteMealPlanCommand(MealPlanId MealPlanId)
{
    public DeleteMealPlanCommand(int mealPlanId)
        : this(new MealPlanId(mealPlanId))
    {
    }
}
