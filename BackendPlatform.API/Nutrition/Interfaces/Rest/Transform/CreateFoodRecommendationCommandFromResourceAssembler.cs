using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class CreateFoodRecommendationCommandFromResourceAssembler
{
    public static CreateFoodRecommendationCommand ToCommandFromResource(CreateFoodRecommendationResource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource),
                "CreateFoodRecommendationResource cannot be null when converting to command.");
        return new CreateFoodRecommendationCommand(resource.MealPlanId, resource.FoodName, resource.Portion,
            resource.NutritionalValue);
    }
}
