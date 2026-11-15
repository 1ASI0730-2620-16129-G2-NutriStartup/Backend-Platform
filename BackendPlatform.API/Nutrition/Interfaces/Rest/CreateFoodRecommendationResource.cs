namespace BackendPlatform.API.Nutrition.Interfaces.Rest;

public record CreateFoodRecommendationResource(int MealPlanId, string FoodName, string Portion,
    string NutritionalValue);
