namespace BackendPlatform.API.Nutrition.Domain.Model;

public enum NutritionError
{
    None,
    InvalidData,
    NutritionPlanNotFound,
    MealPlanNotFound,
    FoodRecommendationNotFound,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
