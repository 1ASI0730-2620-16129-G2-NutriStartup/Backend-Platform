using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Events;

namespace BackendPlatform.API.Nutrition.Domain.Model.Events;

public record FoodRecommendationCreatedEvent(FoodRecommendationId FoodRecommendationId,
    MealPlanId MealPlanId, string FoodName, string Portion, string NutritionalValue) : IEvent;
