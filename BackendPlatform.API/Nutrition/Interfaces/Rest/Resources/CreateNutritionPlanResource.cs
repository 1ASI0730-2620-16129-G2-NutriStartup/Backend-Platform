namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record CreateNutritionPlanResource(string Name, string Description, string Objective,
    string StartDate, string EndDate);
    