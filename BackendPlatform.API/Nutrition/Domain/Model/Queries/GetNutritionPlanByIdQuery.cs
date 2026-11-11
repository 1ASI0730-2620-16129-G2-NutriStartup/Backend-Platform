using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Queries;

public record GetNutritionPlanByIdQuery(NutritionPlanId NutritionPlanId)
{
    public GetNutritionPlanByIdQuery(int nutritionPlanId) : this(new NutritionPlanId(nutritionPlanId))
    {
    }
}
