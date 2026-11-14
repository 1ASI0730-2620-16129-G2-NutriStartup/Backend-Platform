using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when an appointment is moved to another time slot (cita reprogramada).
///     Both the patient and the nutritionist must be notified.
/// </summary>
/// <param name="AppointmentId">The rescheduled appointment.</param>
/// <param name="PatientId">The patient of the appointment.</param>
/// <param name="NutritionistId">The nutritionist assigned to the appointment.</param>
/// <param name="PreviousSlot">The time slot the appointment had before.</param>
/// <param name="NewSlot">The new time slot of the appointment.</param>
public record AppointmentRescheduledEvent(
    AppointmentId AppointmentId,
    PatientId PatientId,
    NutritionistId NutritionistId,
    TimeSlot PreviousSlot,
    TimeSlot NewSlot) : IEvent;
