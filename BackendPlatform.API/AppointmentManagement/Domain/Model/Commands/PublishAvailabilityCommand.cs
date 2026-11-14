using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to publish an availability (publicar disponibilidad).
/// </summary>
/// <param name="NutritionistId">The nutritionist who publishes the availability.</param>
/// <param name="Date">The day of the availability.</param>
/// <param name="StartTime">The time at which the availability starts.</param>
/// <param name="EndTime">The time at which the availability ends.</param>
public record PublishAvailabilityCommand(
    NutritionistId NutritionistId,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime)
{
    public PublishAvailabilityCommand(int nutritionistId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        : this(new NutritionistId(nutritionistId), date, startTime, endTime)
    {
    }
}
