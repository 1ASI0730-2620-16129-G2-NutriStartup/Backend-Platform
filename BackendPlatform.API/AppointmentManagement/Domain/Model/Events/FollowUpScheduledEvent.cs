using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when a follow-up is scheduled after a consultation (seguimiento programado).
/// </summary>
/// <param name="AppointmentId">The appointment that originated the follow-up.</param>
/// <param name="PatientId">The patient who must be followed up.</param>
/// <param name="NutritionistId">The nutritionist in charge of the follow-up.</param>
/// <param name="FollowUpDate">The day of the follow-up.</param>
public record FollowUpScheduledEvent(
    AppointmentId AppointmentId,
    PatientId PatientId,
    NutritionistId NutritionistId,
    DateOnly FollowUpDate) : IEvent;
