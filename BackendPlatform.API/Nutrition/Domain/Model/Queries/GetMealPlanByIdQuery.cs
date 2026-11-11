using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Queries;

public record GetMealPlanByIdQuery(MealPlanId MealPlanId)
{
    public GetMealPlanByIdQuery(int mealPlanId) : this(new MealPlanId(mealPlanId))
    {
    }
}
