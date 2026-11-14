using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Appointment resource for REST API
/// </summary>
/// <param name="Id">The unique identifier of the appointment</param>
/// <param name="UserId">The id of the patient</param>
/// <param name="NutritionistId">The nutritionist assigned to the appointment</param>
/// <param name="AvailabilityId">The availability reserved by the appointment</param>
/// <param name="Date">The day, format yyyy-MM-dd</param>
/// <param name="StartTime">The start time, format HH:mm</param>
/// <param name="EndTime">The end time, format HH:mm</param>
/// <param name="Status">The status: PENDING, CONFIRMED, RESCHEDULED, CANCELLED or COMPLETED</param>
/// <param name="Reason">The reason of the consultation</param>
/// <param name="DataAccessAuthorized">Whether the patient authorized the nutritionist's access</param>
public record AppointmentResource(
    int Id,
    int UserId,
    int NutritionistId,
    int AvailabilityId,
    DateOnly Date,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly StartTime,
    [property: JsonConverter(typeof(TimeOnlyHourMinuteJsonConverter))] TimeOnly EndTime,
    string Status,
    string Reason,
    bool DataAccessAuthorized);
