using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the availability repository in the Appointment Management bounded context.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class AvailabilityRepository(AppDbContext context)
    : BaseRepository<Availability>(context), IAvailabilityRepository
{
    // <inheritdoc />
    public new async Task<Availability?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Availability>()
            .FirstOrDefaultAsync(availability => availability.Id == id, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Availability?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Availability>()
            .AsNoTracking()
            .FirstOrDefaultAsync(availability => availability.Id == id, cancellationToken);
    }

    // <inheritdoc />
    public async Task<Availability?> FindBySlotAsync(NutritionistId nutritionistId, TimeSlot slot,
        CancellationToken cancellationToken)
    {
        var nutritionist = nutritionistId.Value;
        var date = slot.Date;
        var startTime = slot.StartTime;
        var endTime = slot.EndTime;

        return await Context.Set<Availability>()
            .FirstOrDefaultAsync(availability =>
                    availability.NutritionistId == nutritionist
                    && availability.Date == date
                    && availability.StartTime == startTime
                    && availability.EndTime == endTime,
                cancellationToken);
    }

    // <inheritdoc />
    public async Task<bool> ExistsOverlappingAsync(NutritionistId nutritionistId, TimeSlot slot,
        CancellationToken cancellationToken)
    {
        var nutritionist = nutritionistId.Value;
        var date = slot.Date;

        // Overlap is evaluated in memory by the TimeSlot value object; only the same-day candidates are loaded.
        var sameDay = await Context.Set<Availability>()
            .AsNoTracking()
            .Where(availability => availability.NutritionistId == nutritionist && availability.Date == date)
            .ToListAsync(cancellationToken);

        return sameDay.Any(availability => availability.Slot.OverlapsWith(slot));
    }

    // <inheritdoc />
    public async Task<IEnumerable<Availability>> FindByFilterAsNoTrackingAsync(NutritionistId? nutritionistId,
        DateOnly? date, bool onlyAvailable, CancellationToken cancellationToken)
    {
        var query = Context.Set<Availability>().AsNoTracking();

        if (nutritionistId.HasValue)
        {
            var nutritionist = nutritionistId.Value.Value;
            query = query.Where(availability => availability.NutritionistId == nutritionist);
        }

        if (date.HasValue)
        {
            var day = date.Value;
            query = query.Where(availability => availability.Date == day);
        }

        if (onlyAvailable) query = query.Where(availability => availability.IsAvailable);

        return await query
            .OrderBy(availability => availability.Date)
            .ThenBy(availability => availability.StartTime)
            .ToListAsync(cancellationToken);
    }
}
