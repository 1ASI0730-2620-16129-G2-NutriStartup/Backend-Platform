using Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.Shared.Application.Model;

namespace Acme.Center.Platform.AppointmentManagement.Application.CommandServices;

/// <summary>
///     Represents the appointment command service in the Appointment Management bounded context.
/// </summary>
public interface IAppointmentCommandService
{
    /// <summary>
    ///     Handles the book appointment command.
    /// </summary>
    /// <param name="command">The <see cref="BookAppointmentCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The booked <see cref="Appointment" />.</returns>
    Task<Result<Appointment>> Handle(BookAppointmentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the confirm appointment command.
    /// </summary>
    /// <param name="command">The <see cref="ConfirmAppointmentCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The confirmed <see cref="Appointment" />.</returns>
    Task<Result<Appointment>> Handle(ConfirmAppointmentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the reschedule appointment command.
    /// </summary>
    /// <param name="command">The <see cref="RescheduleAppointmentCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The rescheduled <see cref="Appointment" />.</returns>
    Task<Result<Appointment>> Handle(RescheduleAppointmentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the cancel appointment command.
    /// </summary>
    /// <param name="command">The <see cref="CancelAppointmentCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cancelled <see cref="Appointment" />.</returns>
    Task<Result<Appointment>> Handle(CancelAppointmentCommand command, CancellationToken cancellationToken);

    /// <summary>
    ///     Handles the delete appointment command.
    /// </summary>
    /// <param name="command">The <see cref="DeleteAppointmentCommand" /> to handle.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A result without value.</returns>
    Task<Result> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken);
}
