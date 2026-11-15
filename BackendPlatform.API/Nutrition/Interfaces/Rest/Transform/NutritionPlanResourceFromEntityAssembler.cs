using BackendPlatform.API.Nutrition.Domain.Model.Aggregates;
using BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Transform;

public static class NutritionPlanResourceFromEntityAssembler
{
    public static NutritionPlanResource ToResourceFromEntity(NutritionPlan entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity),
                "Nutrition Plan entity cannot be null when converting to resource.");
        return new NutritionPlanResource(entity.Id, entity.Name, entity.Description, entity.Objective,
            entity.StartDate, entity.EndDate, entity.Status.ToString());
    }
}
