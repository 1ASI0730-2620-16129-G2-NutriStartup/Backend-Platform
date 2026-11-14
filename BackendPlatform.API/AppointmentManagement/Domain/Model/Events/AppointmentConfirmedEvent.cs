using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when an appointment is confirmed (cita confirmada).
/// </summary>
/// <param name="AppointmentId">The confirmed appointment.</param>
/// <param name="PatientId">The patient of the appointment.</param>
/// <param name="NutritionistId">The nutritionist assigned to the appointment.</param>
/// <param name="Slot">The time slot of the appointment.</param>
public record AppointmentConfirmedEvent(
    AppointmentId AppointmentId,
    PatientId PatientId,
    NutritionistId NutritionistId,
    TimeSlot Slot) : IEvent;
