using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record DeleteNutritionPlanCommand(NutritionPlanId NutritionPlanId)
{
    public DeleteNutritionPlanCommand(int nutritionPlanId)
        : this(new NutritionPlanId(nutritionPlanId))
    {
    }
}
