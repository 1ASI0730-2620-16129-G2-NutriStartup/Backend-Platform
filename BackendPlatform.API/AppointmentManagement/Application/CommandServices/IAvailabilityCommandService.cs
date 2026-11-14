using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.AppointmentManagement.Application.CommandServices;

/// <summary>
///     Represents the availability command service in the Appointment Management bounded context.
/// </summary>
public interface IAvailabilityCommandService
{
    /// <summary>
    ///     Handles the publish availability command.
    /// </summary>
    /// <param name="command">The <see cref="PublishAvailabilityCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The published <see cref="Availability" />.</returns>
    Task<Result<Availability>> Handle(PublishAvailabilityCommand command, CancellationToken cancellationToken);
}
