using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when a nutritionist publishes a new availability (disponibilidad publicada).
/// </summary>
/// <remarks>
///     Domain events are dispatched right before the INSERT is executed, so the generated identifier of the
///     availability is not yet known. The event therefore identifies the availability by nutritionist and slot.
/// </remarks>
/// <param name="NutritionistId">The nutritionist who published the availability.</param>
/// <param name="Slot">The published time slot.</param>
public record AvailabilityPublishedEvent(NutritionistId NutritionistId, TimeSlot Slot) : IEvent;
