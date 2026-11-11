using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace BackendPlatform.API.Nutrition.Domain.Model.Aggregates;

public class MealPlan : AggregateRoot
{
    public MealPlan(NutritionPlanId nutritionPlanId, string mealType, string description, int calories)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(mealType);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        if (calories < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(calories));
        }
        NutritionPlanId = nutritionPlanId;
        MealType = mealType;
        Description = description;
        Calories = calories;
        AddDomainEvent(new MealPlanCreatedEvent(Id, NutritionPlanId, MealType, Description, Calories));
    }
    
    public MealPlanId Id { get; }
    public NutritionPlanId NutritionPlanId { get; private set; }
    public string MealType { get; private set; }
    public string Description { get; private set; }
    public int Calories { get; private set; }
    public NutritionPlan NutritionPlan { get; internal set; }
}
