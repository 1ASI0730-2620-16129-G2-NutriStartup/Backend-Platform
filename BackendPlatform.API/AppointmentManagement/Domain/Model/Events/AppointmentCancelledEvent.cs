using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when an appointment is cancelled (cita cancelada).
///     Both the patient and the nutritionist must be notified.
/// </summary>
/// <param name="AppointmentId">The cancelled appointment.</param>
/// <param name="PatientId">The patient of the appointment.</param>
/// <param name="NutritionistId">The nutritionist assigned to the appointment.</param>
/// <param name="Slot">The time slot that was released.</param>
/// <param name="Reason">The optional reason of the cancellation.</param>
public record AppointmentCancelledEvent(
    AppointmentId AppointmentId,
    PatientId PatientId,
    NutritionistId NutritionistId,
    TimeSlot Slot,
    string? Reason) : IEvent;
