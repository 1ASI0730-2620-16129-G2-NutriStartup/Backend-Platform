using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Events;

namespace BackendPlatform.API.Nutrition.Domain.Model.Events;

public record MealPlanCreatedEvent(MealPlanId MealPlanId, NutritionPlanId NutritionPlanId, string MealType,
    string Description, int Calories) : IEvent;
    