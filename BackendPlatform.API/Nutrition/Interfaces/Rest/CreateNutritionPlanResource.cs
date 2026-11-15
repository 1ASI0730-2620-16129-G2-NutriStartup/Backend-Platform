namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

public record CreateNutritionPlanResource(string Name, string Description, string Objective,
    string StartDate, string EndDate, string Status);
    