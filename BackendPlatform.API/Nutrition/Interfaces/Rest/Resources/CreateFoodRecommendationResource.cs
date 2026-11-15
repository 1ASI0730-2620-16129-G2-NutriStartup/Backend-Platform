namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record CreateFoodRecommendationResource(int MealPlanId, string FoodName, string Portion,
    string NutritionalValue);
