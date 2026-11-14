using Acme.Center.Platform.AppointmentManagement.Application.CommandServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Errors;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.CommandServices;

/// <summary>
///     Consultation command service
/// </summary>
/// <param name="consultationRepository">Consultation repository</param>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="unitOfWork">Unit of work</param>
public class ConsultationCommandService(
    IConsultationRepository consultationRepository,
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork)
    : IConsultationCommandService
{
    /// <inheritdoc />
    public async Task<Result<Consultation>> Handle(RegisterConsultationCommand command,
        CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId, cancellationToken);
        if (appointment is null)
            return Result<Consultation>.Failure(AppointmentManagementErrors.AppointmentNotFound,
                AppointmentManagementError.AppointmentNotFound);

        if (string.IsNullOrWhiteSpace(command.Notes))
            return Result<Consultation>.Failure(AppointmentManagementErrors.InvalidConsultationData,
                AppointmentManagementError.InvalidConsultationData);

        if (command.FollowUpDate.HasValue && command.FollowUpDate.Value <= command.Date)
            return Result<Consultation>.Failure(AppointmentManagementErrors.InvalidFollowUpDate,
                AppointmentManagementError.InvalidFollowUpDate);

        if (await consultationRepository.ExistsByAppointmentIdAsync(command.AppointmentId, cancellationToken))
            return Result<Consultation>.Failure(AppointmentManagementErrors.ConsultationAlreadyRegistered,
                AppointmentManagementError.ConsultationAlreadyRegistered);

        if (!appointment.CanBeCompleted)
            return Result<Consultation>.Failure(AppointmentManagementErrors.InvalidAppointmentStatus,
                AppointmentManagementError.InvalidAppointmentStatus);

        // Business rule: every attended consultation is recorded in the history, and the appointment is completed
        // in the same unit of work.
        appointment.Complete();
        var consultation = new Consultation(command, appointment);
        await consultationRepository.AddAsync(consultation, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Consultation>.Success(consultation);
    }
}
