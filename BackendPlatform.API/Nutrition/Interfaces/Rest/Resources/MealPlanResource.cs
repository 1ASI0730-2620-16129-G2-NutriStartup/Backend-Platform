namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record MealPlanResource(int Id, NutritionPlanResource NutritionPlan, string MealType,
    string Description, int Calories);
