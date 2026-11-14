using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.Shared.Application.Internal.EventHandlers;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.EventHandlers;

/// <summary>
///     Event handler for <see cref="ConsultationPerformedEvent" />.
/// </summary>
public class ConsultationPerformedEventHandler(ILogger<ConsultationPerformedEventHandler> logger)
    : IEventHandler<ConsultationPerformedEvent>
{
    /// <inheritdoc />
    public Task Handle(ConsultationPerformedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Consultation performed for appointment {AppointmentId} (patient {PatientId}, nutritionist {NutritionistId}) on {Date}",
            domainEvent.AppointmentId.Value, domainEvent.PatientId.Value, domainEvent.NutritionistId.Value,
            domainEvent.Date);
        return Task.CompletedTask;
    }
}
