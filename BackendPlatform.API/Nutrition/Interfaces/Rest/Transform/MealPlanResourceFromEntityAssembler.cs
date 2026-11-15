using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class MealPlanResourceFromEntityAssembler
{
    public static MealPlanResource ToResourceFromEntity(MealPlan entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Meal Plan entity cannot be null when converting to resource.");
        if (entity.NutritionPlan == null)
            throw new InvalidOperationException(
                $"Meal Plan with ID {entity.Id} has a null Nutrition Plan. Ensure Nutrition Plan is properly loaded.");
        return new MealPlanResource(
            entity.Id,
            NutritionPlanResourceFromEntityAssembler.ToResourceFromEntity(entity.NutritionPlan),
            entity.MealType, 
            entity.Description,
            entity.Calories);
    }
}
