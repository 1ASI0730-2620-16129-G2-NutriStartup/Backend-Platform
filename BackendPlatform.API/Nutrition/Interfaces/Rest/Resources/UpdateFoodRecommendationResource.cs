namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record UpdateFoodRecommendationResource(
    int MealPlanId,
    string FoodName,
    string Portion,
    string NutritionalValue);
    