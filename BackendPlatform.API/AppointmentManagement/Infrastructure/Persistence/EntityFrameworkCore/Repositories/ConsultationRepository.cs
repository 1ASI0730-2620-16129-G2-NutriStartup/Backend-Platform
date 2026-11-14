using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Acme.Center.Platform.AppointmentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

/// <summary>
///     Represents the consultation repository in the Appointment Management bounded context.
/// </summary>
/// <param name="context">
///     The <see cref="AppDbContext" /> to use.
/// </param>
public class ConsultationRepository(AppDbContext context)
    : BaseRepository<Consultation>(context), IConsultationRepository
{
    // <inheritdoc />
    public new async Task<Consultation?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Consultation>()
            .FirstOrDefaultAsync(consultation => consultation.Id == id, cancellationToken);
    }

    // <inheritdoc />
    public new async Task<Consultation?> FindByIdAsNoTrackingAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Consultation>()
            .AsNoTracking()
            .FirstOrDefaultAsync(consultation => consultation.Id == id, cancellationToken);
    }

    // <inheritdoc />
    public async Task<bool> ExistsByAppointmentIdAsync(int appointmentId, CancellationToken cancellationToken)
    {
        return await Context.Set<Consultation>()
            .AnyAsync(consultation => consultation.AppointmentId == appointmentId, cancellationToken);
    }

    // <inheritdoc />
    public async Task<IEnumerable<Consultation>> FindByFilterAsNoTrackingAsync(PatientId? patientId,
        NutritionistId? nutritionistId, AppointmentId? appointmentId, CancellationToken cancellationToken)
    {
        var query = Context.Set<Consultation>().AsNoTracking();

        if (patientId.HasValue)
        {
            var patient = patientId.Value.Value;
            query = query.Where(consultation => consultation.PatientId == patient);
        }

        if (nutritionistId.HasValue)
        {
            var nutritionist = nutritionistId.Value.Value;
            query = query.Where(consultation => consultation.NutritionistId == nutritionist);
        }

        if (appointmentId.HasValue)
        {
            var appointment = appointmentId.Value.Value;
            query = query.Where(consultation => consultation.AppointmentId == appointment);
        }

        return await query
            .OrderByDescending(consultation => consultation.Date)
            .ToListAsync(cancellationToken);
    }
}
