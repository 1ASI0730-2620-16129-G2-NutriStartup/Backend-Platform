using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.AppointmentManagement.Application.CommandServices;

/// <summary>
///     Represents the consultation command service in the Appointment Management bounded context.
/// </summary>
public interface IConsultationCommandService
{
    /// <summary>
    ///     Handles the register consultation command.
    /// </summary>
    /// <param name="command">The <see cref="RegisterConsultationCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The registered <see cref="Consultation" />.</returns>
    Task<Result<Consultation>> Handle(RegisterConsultationCommand command, CancellationToken cancellationToken);
}
