namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record UpdateNutritionPlanResource(
    string Name,
    string Description,
    string Objective,
    string StartDate,
    string EndDate,
    string Status);
    