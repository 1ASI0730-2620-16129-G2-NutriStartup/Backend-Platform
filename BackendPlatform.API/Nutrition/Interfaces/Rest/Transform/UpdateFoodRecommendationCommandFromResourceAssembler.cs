using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class UpdateFoodRecommendationCommandFromResourceAssembler
{
    public static UpdateFoodRecommendationCommand ToCommandFromResource(
        UpdateFoodRecommendationResource resource,
        int foodRecommendationId)
    {
        ArgumentNullException.ThrowIfNull(resource);

        return new UpdateFoodRecommendationCommand(
            foodRecommendationId,
            resource.MealPlanId,
            resource.FoodName,
            resource.Portion,
            resource.NutritionalValue);
    }
}
