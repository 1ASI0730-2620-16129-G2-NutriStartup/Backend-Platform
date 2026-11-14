using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when a patient books an appointment (cita reservada).
/// </summary>
/// <remarks>
///     Domain events are dispatched right before the INSERT is executed, so the generated identifier of the
///     appointment is not yet known. The event therefore carries the <see cref="AvailabilityId" /> that was
///     reserved, which uniquely identifies the booking while it is active.
/// </remarks>
/// <param name="PatientId">The patient who booked the appointment.</param>
/// <param name="NutritionistId">The nutritionist assigned to the appointment.</param>
/// <param name="AvailabilityId">The availability that was reserved.</param>
/// <param name="Slot">The booked time slot.</param>
public record AppointmentBookedEvent(
    PatientId PatientId,
    NutritionistId NutritionistId,
    AvailabilityId AvailabilityId,
    TimeSlot Slot) : IEvent;
