using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Aggregates;

public partial class MealPlan
{
    public MealPlan()
    {
        FoodRecommendations = new List<FoodRecommendation>();
    }
    
    public ICollection<FoodRecommendation> FoodRecommendations { get; }
    
    public void RemoveFoodRecommendation(FoodRecommendationId foodRecommendationId)
    {
        var foodRecommendation = FoodRecommendations.FirstOrDefault(a => a.Id == foodRecommendationId);
        if (foodRecommendation is null) return;
        FoodRecommendations.Remove(foodRecommendation);
    }
    
    public void ClearFoodRecommendations()
    {
        FoodRecommendations.Clear();
    }
}
