using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.Aggregates;

public partial class NutritionPlan
{
    public NutritionPlan()
    {
        MealPlans = new List<MealPlan>();
    }
    
    public ICollection<MealPlan> MealPlans { get; }
    
    public void RemoveMealPlan(MealPlanId mealPlanId)
    {
        var mealPlan = MealPlans.FirstOrDefault(a => a.Id == mealPlanId);
        if (mealPlan is null) return;
        MealPlans.Remove(mealPlan);
    }
    
    public void ClearMealPlans()
    {
        MealPlans.Clear();
    }
}
