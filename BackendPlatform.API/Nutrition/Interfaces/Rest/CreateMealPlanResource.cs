namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

public record CreateMealPlanResource(int NutritionPlanId, string MealType, string Description,
    int Calories);
    