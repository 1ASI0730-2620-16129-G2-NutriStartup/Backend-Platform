namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

public record MealPlanResource(int Id, NutritionPlanResource NutritionPlan, string MealType,
    string Description, int Calories);
