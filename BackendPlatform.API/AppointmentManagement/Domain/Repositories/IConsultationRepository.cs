using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Repositories;

/// <summary>
///     Represents the consultation repository in the Appointment Management bounded context.
/// </summary>
public interface IConsultationRepository : IBaseRepository<Consultation>
{
    /// <summary>
    ///     Determines whether a consultation has already been registered for an appointment.
    /// </summary>
    /// <param name="appointmentId">The id of the appointment.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True when the appointment already has a consultation.</returns>
    Task<bool> ExistsByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken);

    /// <summary>
    ///     Finds the consultations that match the given filters without change tracking.
    /// </summary>
    /// <param name="patientId">Optional filter by patient.</param>
    /// <param name="nutritionistId">Optional filter by nutritionist.</param>
    /// <param name="appointmentId">Optional filter by appointment.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching consultations ordered by date, most recent first.</returns>
    Task<IEnumerable<Consultation>> FindByFilterAsNoTrackingAsync(PatientId? patientId,
        NutritionistId? nutritionistId, AppointmentId? appointmentId, CancellationToken cancellationToken);
}
