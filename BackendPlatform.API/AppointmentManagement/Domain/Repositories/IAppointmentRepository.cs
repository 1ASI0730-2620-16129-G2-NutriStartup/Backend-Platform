using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Repositories;

/// <summary>
///     Represents the appointment repository in the Appointment Management bounded context.
/// </summary>
public interface IAppointmentRepository : IBaseRepository<Appointment>
{
    /// <summary>
    ///     Finds the appointments that match the given filters without change tracking.
    /// </summary>
    /// <param name="patientId">Optional filter by patient.</param>
    /// <param name="nutritionistId">Optional filter by nutritionist.</param>
    /// <param name="status">Optional filter by status.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching appointments ordered by date and start time.</returns>
    Task<IEnumerable<Appointment>> FindByFilterAsNoTrackingAsync(PatientId? patientId,
        NutritionistId? nutritionistId, EAppointmentStatus? status, CancellationToken cancellationToken);
}
