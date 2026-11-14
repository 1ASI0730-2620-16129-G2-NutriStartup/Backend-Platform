using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to consult the schedules (consultar horarios).
/// </summary>
/// <param name="NutritionistId">Optional filter by nutritionist.</param>
/// <param name="Date">Optional filter by day.</param>
/// <param name="OnlyAvailable">When true, only the slots that are still free are returned.</param>
public record GetAvailabilitiesQuery(NutritionistId? NutritionistId, DateOnly? Date, bool OnlyAvailable);
