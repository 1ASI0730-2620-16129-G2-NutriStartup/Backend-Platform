using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Queries;

/// <summary>
///     Query to get an availability by its id.
/// </summary>
/// <param name="AvailabilityId">The id of the availability.</param>
public record GetAvailabilityByIdQuery(AvailabilityId AvailabilityId)
{
    public GetAvailabilityByIdQuery(int availabilityId) : this(new AvailabilityId(availabilityId))
    {
    }
}
