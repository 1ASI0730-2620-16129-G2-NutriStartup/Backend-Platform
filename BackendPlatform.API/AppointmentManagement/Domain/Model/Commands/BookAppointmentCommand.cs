using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;

/// <summary>
///     Command to book an appointment (reservar cita) in a published availability.
/// </summary>
/// <param name="PatientId">The patient who books the appointment.</param>
/// <param name="NutritionistId">The nutritionist the appointment is booked with.</param>
/// <param name="Date">The day of the availability to book.</param>
/// <param name="StartTime">The start time of the availability to book.</param>
/// <param name="EndTime">The end time of the availability to book.</param>
/// <param name="Reason">The reason of the consultation.</param>
/// <param name="AuthorizeDataAccess">
///     Whether the patient authorizes the nutritionist to access their information.
/// </param>
public record BookAppointmentCommand(
    PatientId PatientId,
    NutritionistId NutritionistId,
    DateOnly Date,
    TimeOnly StartTime,
    TimeOnly EndTime,
    string Reason,
    bool AuthorizeDataAccess)
{
    public BookAppointmentCommand(int patientId, int nutritionistId, DateOnly date, TimeOnly startTime,
        TimeOnly endTime, string reason, bool authorizeDataAccess)
        : this(new PatientId(patientId), new NutritionistId(nutritionistId), date, startTime, endTime, reason,
            authorizeDataAccess)
    {
    }
}
