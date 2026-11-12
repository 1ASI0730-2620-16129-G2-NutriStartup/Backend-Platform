using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Queries;

public record GetAllFoodRecommendationsByMealPlanId(MealPlanId MealPlanId)
{
    public GetAllFoodRecommendationsByMealPlanId(int mealPlanId)
        : this(new MealPlanId(mealPlanId))
    {
    }
}
