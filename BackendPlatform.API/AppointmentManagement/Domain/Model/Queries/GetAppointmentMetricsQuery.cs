using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to calculate the verification metrics of the appointments.
/// </summary>
/// <param name="NutritionistId">Optional filter by nutritionist.</param>
public record GetAppointmentMetricsQuery(NutritionistId? NutritionistId);
