using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to register the consultation that took place in an appointment (registrar atención).
/// </summary>
/// <param name="AppointmentId">The appointment that was attended.</param>
/// <param name="Notes">The notes taken by the nutritionist.</param>
/// <param name="Recommendations">The recommendations given to the patient.</param>
/// <param name="Date">The day the consultation took place.</param>
/// <param name="FollowUpDate">The optional day of the follow-up (seguimiento).</param>
public record RegisterConsultationCommand(
    AppointmentId AppointmentId,
    string Notes,
    string? Recommendations,
    DateOnly Date,
    DateOnly? FollowUpDate)
{
    public RegisterConsultationCommand(int appointmentId, string notes, string? recommendations, DateOnly date,
        DateOnly? followUpDate)
        : this(new AppointmentId(appointmentId), notes, recommendations, date, followUpDate)
    {
    }
}
