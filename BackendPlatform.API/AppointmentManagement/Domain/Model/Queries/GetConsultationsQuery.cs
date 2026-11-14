using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to list the consultations of the history.
/// </summary>
/// <param name="PatientId">Optional filter by patient.</param>
/// <param name="NutritionistId">Optional filter by nutritionist.</param>
/// <param name="AppointmentId">Optional filter by appointment.</param>
public record GetConsultationsQuery(PatientId? PatientId, NutritionistId? NutritionistId, AppointmentId? AppointmentId);
