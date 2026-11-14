using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Book appointment resource for REST API
/// </summary>
/// <param name="UserId">The id of the patient who books the appointment</param>
/// <param name="NutritionistId">The nutritionist the appointment is booked with</param>
/// <param name="Date">The day of the availability, format yyyy-MM-dd</param>
/// <param name="StartTime">The start time of the availability, format HH:mm</param>
/// <param name="EndTime">The end time of the availability, format HH:mm</param>
/// <param name="Reason">The reason of the consultation</param>
/// <param name="AuthorizeDataAccess">Whether the patient authorizes the nutritionist to access their information</param>
public record BookAppointmentResource(
    [property: Range(1, int.MaxValue)] int UserId,
    [property: Range(1, int.MaxValue)] int NutritionistId,
    DateOnly Date,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly StartTime,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly EndTime,
    [property: Required, MaxLength(500)] string Reason,
    bool AuthorizeDataAccess);
