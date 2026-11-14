using Acme.Center.Platform.AppointmentManagement.Application.QueryServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.QueryServices;

/// <summary>
///     Appointment query service
/// </summary>
/// <param name="appointmentRepository">Appointment repository</param>
public class AppointmentQueryService(IAppointmentRepository appointmentRepository) : IAppointmentQueryService
{
    /// <inheritdoc />
    public async Task<Appointment?> Handle(GetAppointmentByIdQuery query, CancellationToken cancellationToken)
    {
        return await appointmentRepository.FindByIdAsNoTrackingAsync(query.AppointmentId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Appointment>> Handle(GetAppointmentsQuery query,
        CancellationToken cancellationToken)
    {
        return await appointmentRepository.FindByFilterAsNoTrackingAsync(query.PatientId, query.NutritionistId,
            query.Status, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AppointmentMetrics> Handle(GetAppointmentMetricsQuery query,
        CancellationToken cancellationToken)
    {
        var appointments = await appointmentRepository.FindByFilterAsNoTrackingAsync(null, query.NutritionistId,
            null, cancellationToken);
        return AppointmentMetrics.From(appointments);
    }
}
