using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Reschedule appointment resource for REST API
/// </summary>
/// <param name="Date">The day of the new availability, format yyyy-MM-dd</param>
/// <param name="StartTime">The start time of the new availability, format HH:mm</param>
/// <param name="EndTime">The end time of the new availability, format HH:mm</param>
public record RescheduleAppointmentResource(
    DateOnly Date,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly StartTime,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly EndTime);
