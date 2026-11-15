using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Queries;

public record GetAllFoodRecommendationsByMealPlanIdQuery(MealPlanId MealPlanId)
{
    public GetAllFoodRecommendationsByMealPlanIdQuery(int mealPlanId)
        : this(new MealPlanId(mealPlanId))
    {
    }
}
