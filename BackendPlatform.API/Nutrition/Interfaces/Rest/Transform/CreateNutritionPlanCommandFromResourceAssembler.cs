using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class CreateNutritionPlanCommandFromResourceAssembler
{
    public static CreateNutritionPlanCommand ToCommandFromResource(CreateNutritionPlanResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateNutritionPlanResource cannot be null when converting to command.");
        return new CreateNutritionPlanCommand(resource.Name, resource.Description, resource.Objective,
            resource.StartDate, resource.EndDate);
    }
}
