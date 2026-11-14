using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Register consultation resource for REST API
/// </summary>
/// <param name="AppointmentId">The appointment that was attended</param>
/// <param name="Notes">The notes taken by the nutritionist</param>
/// <param name="Recommendations">The recommendations given to the patient</param>
/// <param name="Date">The day of the consultation, format yyyy-MM-dd</param>
/// <param name="FollowUpDate">The optional day of the follow-up, format yyyy-MM-dd</param>
public record RegisterConsultationResource(
    [property: Range(1, int.MaxValue)] int AppointmentId,
    [property: Required, MaxLength(2000)] string Notes,
    [property: MaxLength(2000)] string? Recommendations,
    DateOnly Date,
    DateOnly? FollowUpDate);
