namespace BackendPlatform.API.Nutrition.Interfaces.Rest.Resources;

public record FoodRecommendationResource(int Id, MealPlanResource MealPlan, string FoodName, string Portion,
    string NutritionalValue);
    