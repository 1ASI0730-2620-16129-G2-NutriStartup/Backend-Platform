using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Availability resource for REST API
/// </summary>
/// <param name="Id">The unique identifier of the availability</param>
/// <param name="NutritionistId">The nutritionist who published the availability</param>
/// <param name="Date">The day, format yyyy-MM-dd</param>
/// <param name="StartTime">The start time, format HH:mm</param>
/// <param name="EndTime">The end time, format HH:mm</param>
/// <param name="Available">Whether the slot is still free</param>
public record AvailabilityResource(
    int Id,
    int NutritionistId,
    DateOnly Date,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly StartTime,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly EndTime,
    bool Available);
