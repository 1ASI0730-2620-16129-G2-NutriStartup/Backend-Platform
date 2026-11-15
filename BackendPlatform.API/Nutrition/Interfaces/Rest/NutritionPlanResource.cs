namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

public record NutritionPlanResource(int Id, string Name, string Description, string Objective,
    string StartDate, string EndDate, string Status);
    