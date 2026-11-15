using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Shared.Application.Internal.EventHandlers;
using Microsoft.Extensions.Logging;

namespace BackendPlatform.API.Nutrition.Application.Internal.EventHandlers;

public class NutritionPlanCreatedEventHandler(ILogger<NutritionPlanCreatedEventHandler> logger) : IEventHandler<NutritionPlanCreatedEvent>
{
    public Task Handle(NutritionPlanCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Created Nutrition Plan: {Name}", domainEvent.Name);
        return Task.CompletedTask;
    }
}
