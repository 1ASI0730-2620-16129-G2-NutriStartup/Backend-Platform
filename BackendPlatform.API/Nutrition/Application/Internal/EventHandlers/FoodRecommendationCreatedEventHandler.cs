using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Shared.Application.Internal.EventHandlers;
using Microsoft.Extensions.Logging;

namespace BackendPlatform.API.Nutrition.Application.Internal.EventHandlers;

public class FoodRecommendationCreatedEventHandler(ILogger<FoodRecommendationCreatedEventHandler> logger) : IEventHandler<FoodRecommendationCreatedEvent>
{
    public Task Handle(FoodRecommendationCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Created Food Recommendation: {FoodName}", domainEvent.FoodName);
        return Task.CompletedTask;
    }
}
