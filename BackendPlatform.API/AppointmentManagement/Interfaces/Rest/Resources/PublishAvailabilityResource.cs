using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Publish availability resource for REST API
/// </summary>
/// <param name="NutritionistId">The nutritionist who publishes the availability</param>
/// <param name="Date">The day, format yyyy-MM-dd</param>
/// <param name="StartTime">The start time, format HH:mm</param>
/// <param name="EndTime">The end time, format HH:mm</param>
public record PublishAvailabilityResource(
    [property: Range(1, int.MaxValue)] int NutritionistId,
    DateOnly Date,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly StartTime,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly EndTime);
