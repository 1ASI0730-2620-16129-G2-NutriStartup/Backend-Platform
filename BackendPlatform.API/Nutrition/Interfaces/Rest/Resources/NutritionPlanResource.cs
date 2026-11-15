namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record NutritionPlanResource(int Id, string Name, string Description, string Objective,
    string StartDate, string EndDate, string Status);
    