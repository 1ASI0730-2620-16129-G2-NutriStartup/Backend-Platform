using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record UpdateNutritionPlanCommand(
    NutritionPlanId NutritionPlanId,
    string Name,
    string Description,
    string Objective,
    string StartDate,
    string EndDate,
    string Status)
{
    public UpdateNutritionPlanCommand(
        int nutritionPlanId,
        string name,
        string description,
        string objective,
        string startDate,
        string endDate,
        string status)
        : this(
            new NutritionPlanId(nutritionPlanId),
            name,
            description,
            objective,
            startDate,
            endDate,
            status)
    {
    }
}
