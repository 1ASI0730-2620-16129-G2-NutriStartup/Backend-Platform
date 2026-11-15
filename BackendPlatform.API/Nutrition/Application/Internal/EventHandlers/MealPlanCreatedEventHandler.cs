using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Shared.Application.Internal.EventHandlers;
using Microsoft.Extensions.Logging;

namespace BackendPlatform.API.Nutrition.Application.Internal.EventHandlers;

public class MealPlanCreatedEventHandler(ILogger<MealPlanCreatedEventHandler> logger) : IEventHandler<MealPlanCreatedEvent>
{
    public Task Handle(MealPlanCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Created Meal Plan: {Description}", domainEvent.Description);
        return Task.CompletedTask;
    }
}
