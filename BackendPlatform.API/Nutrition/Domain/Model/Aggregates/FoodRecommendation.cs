using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace BackendPlatform.API.Nutrition.Domain.Model.Aggregates;

public class FoodRecommendation : AggregateRoot
{
    public FoodRecommendation(MealPlanId mealPlanId, string foodName, string portion,
        string nutritionalValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(foodName);
        ArgumentException.ThrowIfNullOrWhiteSpace(portion);
        ArgumentException.ThrowIfNullOrWhiteSpace(nutritionalValue);
        MealPlanId = mealPlanId;
        FoodName = foodName;
        Portion = portion;
        NutritionalValue = nutritionalValue;
        AddDomainEvent(new FoodRecommendationCreatedEvent(Id, MealPlanId, FoodName, Portion,
            NutritionalValue));
    }
    
    public FoodRecommendation(CreateFoodRecommendationCommand command) : this(command.MealPlanId,
        command.FoodName, command.Portion, command.NutritionalValue)
    {
        
    }
    
    public FoodRecommendationId Id { get; }
    public MealPlanId MealPlanId { get; private set; }
    public string FoodName { get; private set; }
    public string Portion { get; private set; }
    public string NutritionalValue { get; private set; }
    public MealPlan MealPlan { get; internal set; } = null!;
}
