using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class UpdateMealPlanCommandFromResourceAssembler
{
    public static UpdateMealPlanCommand ToCommandFromResource(
        UpdateMealPlanResource resource,
        int mealPlanId)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new UpdateMealPlanCommand(
            mealPlanId,
            resource.NutritionPlanId,
            resource.MealType,
            resource.Description,
            resource.Calories);
    }
}
