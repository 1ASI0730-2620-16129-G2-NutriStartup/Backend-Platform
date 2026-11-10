using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Events;

namespace BackendPlatform.API.Nutrition.Domain.Model.Events;

public record NutritionPlanCreatedEvent(NutritionPlanId NutritionPlanId, string Name, string Description,
    string Objective, string StartDate, string EndDate, string Status) : IEvent;
    