using Acme.Center.Platform.AppointmentManagement.Application.CommandServices;
using Acme.Center.Platform.AppointmentManagement.Domain.Model;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Errors;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.AppointmentManagement.Domain.Repositories;
using Acme.Center.Platform.Shared.Application.Model;
using Acme.Center.Platform.Shared.Domain.Repositories;

namespace Acme.Center.Platform.AppointmentManagement.Application.Internal.CommandServices;

/// <summary>
///     Availability command service
/// </summary>
/// <param name="availabilityRepository">Availability repository</param>
/// <param name="unitOfWork">Unit of work</param>
public class AvailabilityCommandService(
    IAvailabilityRepository availabilityRepository,
    IUnitOfWork unitOfWork)
    : IAvailabilityCommandService
{
    /// <inheritdoc />
    public async Task<Result<Availability>> Handle(PublishAvailabilityCommand command,
        CancellationToken cancellationToken)
    {
        var slot = new TimeSlot(command.Date, command.StartTime, command.EndTime);
        if (!slot.IsValid)
            return Result<Availability>.Failure(AppointmentManagementErrors.InvalidTimeSlot,
                AppointmentManagementError.InvalidTimeSlot);

        if (await availabilityRepository.ExistsOverlappingAsync(command.NutritionistId, slot, cancellationToken))
            return Result<Availability>.Failure(AppointmentManagementErrors.AvailabilityOverlap,
                AppointmentManagementError.AvailabilityOverlap);

        var availability = new Availability(command);
        await availabilityRepository.AddAsync(availability, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Availability>.Success(availability);
    }
}
