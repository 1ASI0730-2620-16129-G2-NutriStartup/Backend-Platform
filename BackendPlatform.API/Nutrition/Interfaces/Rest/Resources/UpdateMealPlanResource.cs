namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record UpdateMealPlanResource(
    int NutritionPlanId,
    string MealType,
    string Description,
    int Calories);
    