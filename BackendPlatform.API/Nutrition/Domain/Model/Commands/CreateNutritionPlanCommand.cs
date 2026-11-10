namespace BackendPlatform.API.Nutrition.Domain.Model.Commands;

public record CreateNutritionPlanCommand(
    string Name,
    string Description,
    string Objective,
    string StartDate,
    string EndDate,
    string Status);
