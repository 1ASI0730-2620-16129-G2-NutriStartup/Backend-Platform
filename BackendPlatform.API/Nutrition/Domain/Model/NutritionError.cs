namespace BackendPlatform.API.Nutrition.Domain.Model;

public enum NutritionError
{
    None,
    NutritionPlanNotFound,
    MealPlanNotFound,
    FoodRecommendationNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
