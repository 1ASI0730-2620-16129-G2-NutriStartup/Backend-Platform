using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to list the appointments of the agenda.
/// </summary>
/// <param name="PatientId">Optional filter by patient.</param>
/// <param name="NutritionistId">Optional filter by nutritionist.</param>
/// <param name="Status">Optional filter by appointment status.</param>
public record GetAppointmentsQuery(PatientId? PatientId, NutritionistId? NutritionistId, EAppointmentStatus? Status);
