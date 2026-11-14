using Acme.Center.Platform.AppointmentManagement.Application.QueryServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.QueryServices;

/// <summary>
///     Consultation query service
/// </summary>
/// <param name="consultationRepository">Consultation repository</param>
public class ConsultationQueryService(IConsultationRepository consultationRepository) : IConsultationQueryService
{
    /// <inheritdoc />
    public async Task<Consultation?> Handle(GetConsultationByIdQuery query, CancellationToken cancellationToken)
    {
        return await consultationRepository.FindByIdAsNoTrackingAsync(query.ConsultationId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Consultation>> Handle(GetConsultationsQuery query,
        CancellationToken cancellationToken)
    {
        return await consultationRepository.FindByFilterAsNoTrackingAsync(query.PatientId, query.NutritionistId,
            query.AppointmentId, cancellationToken);
    }
}
