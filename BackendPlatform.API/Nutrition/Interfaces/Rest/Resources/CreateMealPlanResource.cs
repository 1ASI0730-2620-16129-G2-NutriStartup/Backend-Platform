namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record CreateMealPlanResource(int NutritionPlanId, string MealType, string Description,
    int Calories);
    