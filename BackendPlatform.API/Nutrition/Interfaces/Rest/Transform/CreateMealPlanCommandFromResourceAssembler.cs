using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class CreateMealPlanCommandFromResourceAssembler
{
    public static CreateMealPlanCommand ToCommandFromResource(CreateMealPlanResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateMealPlanResource cannot be null when converting to command.");
        return new CreateMealPlanCommand(resource.NutritionPlanId, resource.MealType, resource.Description,
            resource.Calories);
    }
}
