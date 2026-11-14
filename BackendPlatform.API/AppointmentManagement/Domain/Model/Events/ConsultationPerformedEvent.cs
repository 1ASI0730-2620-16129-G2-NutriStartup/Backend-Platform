using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Events;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;

/// <summary>
///     Domain event raised when a consultation is registered in the history (consulta realizada).
/// </summary>
/// <param name="AppointmentId">The appointment that was attended.</param>
/// <param name="PatientId">The patient who was attended.</param>
/// <param name="NutritionistId">The nutritionist who performed the consultation.</param>
/// <param name="Date">The day the consultation took place.</param>
public record ConsultationPerformedEvent(
    AppointmentId AppointmentId,
    PatientId PatientId,
    NutritionistId NutritionistId,
    DateOnly Date) : IEvent;
