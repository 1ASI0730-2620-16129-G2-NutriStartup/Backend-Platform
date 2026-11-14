using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Converters;

namespace Acme.Center.Platform.AppointmentManagement.Interfaces.Rest.Resources;

/// <summary>
///     Consultation resource for REST API
/// </summary>
/// <param name="Id">The unique identifier of the consultation</param>
/// <param name="AppointmentId">The appointment that was attended</param>
/// <param name="UserId">The id of the patient</param>
/// <param name="NutritionistId">The nutritionist who performed the consultation</param>
/// <param name="Notes">The notes taken by the nutritionist</param>
/// <param name="Recommendations">The recommendations given to the patient</param>
/// <param name="Date">The day of the consultation</param>
/// <param name="FollowUpDate">The optional day of the follow-up</param>
public record ConsultationResource(
    int Id,
    int AppointmentId,
    int UserId,
    int NutritionistId,
    string Notes,
    string Recommendations,
    DateOnly Date,
    DateOnly? FollowUpDate);
