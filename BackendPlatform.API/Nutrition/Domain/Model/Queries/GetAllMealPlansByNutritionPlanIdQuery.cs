using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Queries;

public record GetAllMealPlansByNutritionPlanIdQuery(NutritionPlanId NutritionPlanId)
{
    public GetAllMealPlansByNutritionPlanIdQuery(int nutritionPlanId)
        : this(new NutritionPlanId(nutritionPlanId))
    {
    }
}