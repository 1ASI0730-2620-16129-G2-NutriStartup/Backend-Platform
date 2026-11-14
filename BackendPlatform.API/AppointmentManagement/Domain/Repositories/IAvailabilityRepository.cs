using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Repositories;

/// <summary>
///     Represents the availability repository in the Appointment Management bounded context.
/// </summary>
public interface IAvailabilityRepository : IBaseRepository<Availability>
{
    /// <summary>
    ///     Finds the availability of a nutritionist that matches exactly the given slot.
    /// </summary>
    /// <param name="nutritionistId">The nutritionist who published the availability.</param>
    /// <param name="slot">The time slot to look for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The tracked availability if found, otherwise null.</returns>
    Task<Availability?> FindBySlotAsync(NutritionistId nutritionistId, TimeSlot slot,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Determines whether the nutritionist already has an availability that overlaps with the given slot.
    /// </summary>
    /// <param name="nutritionistId">The nutritionist who publishes the availability.</param>
    /// <param name="slot">The time slot to check.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>True when an overlapping availability exists.</returns>
    Task<bool> ExistsOverlappingAsync(NutritionistId nutritionistId, TimeSlot slot,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Finds the availabilities that match the given filters without change tracking.
    /// </summary>
    /// <param name="nutritionistId">Optional filter by nutritionist.</param>
    /// <param name="date">Optional filter by day.</param>
    /// <param name="onlyAvailable">When true, only free slots are returned.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The matching availabilities ordered by date and start time.</returns>
    Task<IEnumerable<Availability>> FindByFilterAsNoTrackingAsync(NutritionistId? nutritionistId, DateOnly? date,
        bool onlyAvailable, CancellationToken cancellationToken);
}
