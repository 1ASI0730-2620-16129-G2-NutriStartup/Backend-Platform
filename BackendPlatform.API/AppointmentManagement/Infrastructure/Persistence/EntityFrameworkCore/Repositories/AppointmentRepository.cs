using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the appointment repository in the Appointment Management bounded context.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class AppointmentRepository(AppDbContext context)
    : BaseRepository<Appointment>(context), IAppointmentRepository
{
    // <inheritdoc />
    public new async Task<Appointment?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Appointment>()
            .FirstOrDefaultAsync(appointment => appointment.Id == id, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Appointment?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Appointment>()
            .AsNoTracking()
            .FirstOrDefaultAsync(appointment => appointment.Id == id, cancellationToken);
    }

    // <inheritdoc />
    public async Task<IEnumerable<Appointment>> FindByFilterAsNoTrackingAsync(PatientId? patientId,
        NutritionistId? nutritionistId, EAppointmentStatus? status, CancellationToken cancellationToken)
    {
        var query = Context.Set<Appointment>().AsNoTracking();

        if (patientId.HasValue)
        {
            var patient = patientId.Value.Value;
            query = query.Where(appointment => appointment.PatientId == patient);
        }

        if (nutritionistId.HasValue)
        {
            var nutritionist = nutritionistId.Value.Value;
            query = query.Where(appointment => appointment.NutritionistId == nutritionist);
        }

        if (status.HasValue)
        {
            var appointmentStatus = status.Value;
            query = query.Where(appointment => appointment.Status == appointmentStatus);
        }

        return await query
            .OrderBy(appointment => appointment.Date)
            .ThenBy(appointment => appointment.StartTime)
            .ToListAsync(cancellationToken);
    }
}
