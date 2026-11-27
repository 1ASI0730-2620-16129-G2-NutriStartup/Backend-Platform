using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class UpdateNutritionPlanCommandFromResourceAssembler
{
    public static UpdateNutritionPlanCommand ToCommandFromResource(
        UpdateNutritionPlanResource resource,
        int nutritionPlanId)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new UpdateNutritionPlanCommand(
            nutritionPlanId,
            resource.Name,
            resource.Description,
            resource.Objective,
            resource.StartDate,
            resource.EndDate,
            resource.Status);
    }
}
