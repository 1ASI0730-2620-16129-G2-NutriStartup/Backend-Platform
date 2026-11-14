using Acme.Center.Platform.AppointmentManagement.Application.QueryServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.QueryServices;

/// <summary>
///     Availability query service
/// </summary>
/// <param name="availabilityRepository">Availability repository</param>
public class AvailabilityQueryService(IAvailabilityRepository availabilityRepository) : IAvailabilityQueryService
{
    /// <inheritdoc />
    public async Task<Availability?> Handle(GetAvailabilityByIdQuery query, CancellationToken cancellationToken)
    {
        return await availabilityRepository.FindByIdAsNoTrackingAsync(query.AvailabilityId, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Availability>> Handle(GetAvailabilitiesQuery query,
        CancellationToken cancellationToken)
    {
        return await availabilityRepository.FindByFilterAsNoTrackingAsync(query.NutritionistId, query.Date,
            query.OnlyAvailable, cancellationToken);
    }
}
