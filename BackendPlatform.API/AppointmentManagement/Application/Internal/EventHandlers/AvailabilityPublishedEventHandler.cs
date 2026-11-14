using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="AvailabilityPublishedEvent" />.
/// </summary>
public class AvailabilityPublishedEventHandler(ILogger<AvailabilityPublishedEventHandler> logger)
    : IEventHandler<AvailabilityPublishedEvent>
{
    /// <inheritdoc />
    public Task Handle(AvailabilityPublishedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Availability published by nutritionist {NutritionistId}: {Slot}",
            domainEvent.NutritionistId.Value, domainEvent.Slot.Describe());
        return Task.CompletedTask;
    }
}
