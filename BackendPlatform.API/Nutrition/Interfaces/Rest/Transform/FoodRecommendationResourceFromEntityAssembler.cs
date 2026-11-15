using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public class FoodRecommendationResourceFromEntityAssembler
{
    public static FoodRecommendationResource ToResourceFromEntity(FoodRecommendation entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Food Recommendation entity cannot be null when converting to resource.");
        if (entity.MealPlan == null)
            throw new InvalidOperationException(
                $"Food Recommendation with ID {entity.Id} has a null Meal Plan. Ensure Meal Plan is properly loaded.");
        return new FoodRecommendationResource(
            entity.Id,
            MealPlanResourceFromEntityAssembler.ToResourceFromEntity(entity.MealPlan),
            entity.FoodName,
            entity.Portion,
            entity.NutritionalValue);
    }
}
