using Acme.Center.Platform.AppointmentManagement.Application.CommandServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Errors;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.CommandServices;

/// <summary>
///     Appointment command service. It coordinates the <see cref="Appointment" /> and <see cref="Availability" />
///     aggregates inside one unit of work.
/// </summary>
/// <param name="appointmentRepository">Appointment repository</param>
/// <param name="availabilityRepository">Availability repository</param>
/// <param name="unitOfWork">Unit of work</param>
public class AppointmentCommandService(
    IAppointmentRepository appointmentRepository,
    IAvailabilityRepository availabilityRepository,
    IUnitOfWork unitOfWork)
    : IAppointmentCommandService
{
    /// <inheritdoc />
    public async Task<Result<Appointment>> Handle(BookAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        // Business rule: the patient must authorize the nutritionist's access to their information.
        if (!command.AuthorizeDataAccess)
            return Failure(AppointmentManagementErrors.DataAccessNotAuthorized,
                AppointmentManagementError.DataAccessNotAuthorized);

        if (string.IsNullOrWhiteSpace(command.Reason))
            return Failure(AppointmentManagementErrors.InvalidAppointmentData,
                AppointmentManagementError.InvalidAppointmentData);

        var slot = new TimeSlot(command.Date, command.StartTime, command.EndTime);
        if (!slot.IsValid)
            return Failure(AppointmentManagementErrors.InvalidTimeSlot, AppointmentManagementError.InvalidTimeSlot);

        // Business rule: an appointment can only be booked inside a published availability...
        var availability =
            await availabilityRepository.FindBySlotAsync(command.NutritionistId, slot, cancellationToken);
        if (availability is null)
            return Failure(AppointmentManagementErrors.AvailabilityNotFound,
                AppointmentManagementError.AvailabilityNotFound);

        // ...and the same slot can never be assigned to two patients.
        if (!availability.IsAvailable)
            return Failure(AppointmentManagementErrors.SlotNotAvailable,
                AppointmentManagementError.SlotNotAvailable);

        availability.Reserve();
        var appointment = new Appointment(command, availability.Id);
        await appointmentRepository.AddAsync(appointment, cancellationToken);

        if (!await TrySaveAsync(cancellationToken))
            return Failure(AppointmentManagementErrors.SlotNotAvailable,
                AppointmentManagementError.SlotNotAvailable);

        return Result<Appointment>.Success(appointment);
    }

    /// <inheritdoc />
    public async Task<Result<Appointment>> Handle(ConfirmAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId, cancellationToken);
        if (appointment is null)
            return Failure(AppointmentManagementErrors.AppointmentNotFound,
                AppointmentManagementError.AppointmentNotFound);

        if (!appointment.CanBeConfirmed)
            return Failure(AppointmentManagementErrors.InvalidAppointmentStatus,
                AppointmentManagementError.InvalidAppointmentStatus);

        appointment.Confirm();
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Appointment>.Success(appointment);
    }

    /// <inheritdoc />
    public async Task<Result<Appointment>> Handle(RescheduleAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId, cancellationToken);
        if (appointment is null)
            return Failure(AppointmentManagementErrors.AppointmentNotFound,
                AppointmentManagementError.AppointmentNotFound);

        if (!appointment.CanBeRescheduled)
            return Failure(AppointmentManagementErrors.InvalidAppointmentStatus,
                AppointmentManagementError.InvalidAppointmentStatus);

        var newSlot = new TimeSlot(command.NewDate, command.NewStartTime, command.NewEndTime);
        if (!newSlot.IsValid)
            return Failure(AppointmentManagementErrors.InvalidTimeSlot, AppointmentManagementError.InvalidTimeSlot);

        // The assigned nutritionist does not change: the new slot must belong to the same nutritionist.
        var newAvailability =
            await availabilityRepository.FindBySlotAsync(appointment.NutritionistId, newSlot, cancellationToken);
        if (newAvailability is null)
            return Failure(AppointmentManagementErrors.AvailabilityNotFound,
                AppointmentManagementError.AvailabilityNotFound);

        if (!newAvailability.IsAvailable)
            return Failure(AppointmentManagementErrors.SlotNotAvailable,
                AppointmentManagementError.SlotNotAvailable);

        var previousAvailability =
            await availabilityRepository.FindByIdAsync(appointment.AvailabilityId, cancellationToken);

        newAvailability.Reserve();
        previousAvailability?.Release();
        appointment.Reschedule(newAvailability.Id, newSlot);

        if (!await TrySaveAsync(cancellationToken))
            return Failure(AppointmentManagementErrors.SlotNotAvailable,
                AppointmentManagementError.SlotNotAvailable);

        return Result<Appointment>.Success(appointment);
    }

    /// <inheritdoc />
    public async Task<Result<Appointment>> Handle(CancelAppointmentCommand command,
        CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId, cancellationToken);
        if (appointment is null)
            return Failure(AppointmentManagementErrors.AppointmentNotFound,
                AppointmentManagementError.AppointmentNotFound);

        if (!appointment.CanBeCancelled)
            return Failure(AppointmentManagementErrors.InvalidAppointmentStatus,
                AppointmentManagementError.InvalidAppointmentStatus);

        var availability = await availabilityRepository.FindByIdAsync(appointment.AvailabilityId, cancellationToken);

        appointment.Cancel(command.Reason);
        availability?.Release();
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Appointment>.Success(appointment);
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = await appointmentRepository.FindByIdAsync(command.AppointmentId, cancellationToken);
        if (appointment is null)
            return Result.Failure(AppointmentManagementErrors.AppointmentNotFound,
                AppointmentManagementError.AppointmentNotFound);

        if (!appointment.CanBeDeleted)
            return Result.Failure(AppointmentManagementErrors.AppointmentCannotBeDeleted,
                AppointmentManagementError.AppointmentCannotBeDeleted);

        appointmentRepository.Remove(appointment);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }

    private static Result<Appointment> Failure(Acme.Center.Platform.Shared.Domain.Model.Errors.Error error,
        AppointmentManagementError errorCode)
    {
        return Result<Appointment>.Failure(error, errorCode);
    }

    /// <summary>
    ///     Saves the changes. Returns false when another request reserved the same slot first
    ///     (optimistic concurrency on <see cref="Availability.IsAvailable" />).
    /// </summary>
    private async Task<bool> TrySaveAsync(CancellationToken cancellationToken)
    {
        try
        {
            await unitOfWork.CompleteAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateConcurrencyException)
        {
            return false;
        }
    }
}
