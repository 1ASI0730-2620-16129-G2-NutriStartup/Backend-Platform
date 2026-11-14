using Acme.Center.Platform.AppointmentManagement.Domain.Model.Commands;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.Events;
using Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;
using Acme.Center.Platform.Shared.Domain.Model.Aggregates.Internal;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.Aggregates;

/// <summary>
///     Represents the Availability aggregate root within the Appointment Management Bounded Context.
/// </summary>
/// <remarks>
///     An availability (disponibilidad) is a time slot that a nutritionist publishes so that patients can book
///     it. A slot can only be reserved by one appointment at a time: that is the invariant this aggregate guards.
/// </remarks>
public partial class Availability : AggregateRoot
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Availability" /> aggregate with default values.
    ///     This constructor is primarily for persistence mechanisms.
    /// </summary>
    public Availability()
    {
        IsAvailable = true;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Availability" /> aggregate.
    /// </summary>
    /// <param name="nutritionistId">The nutritionist who publishes the availability.</param>
    /// <param name="slot">The published time slot.</param>
    public Availability(NutritionistId nutritionistId, TimeSlot slot) : this()
    {
        ArgumentNullException.ThrowIfNull(slot);
        if (!slot.IsValid)
            throw new ArgumentException("The end time must be later than the start time.", nameof(slot));

        NutritionistId = nutritionistId;
        Date = slot.Date;
        StartTime = slot.StartTime;
        EndTime = slot.EndTime;
        AddDomainEvent(new AvailabilityPublishedEvent(nutritionistId, slot));
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Availability" /> aggregate using a
    ///     <see cref="PublishAvailabilityCommand" />.
    /// </summary>
    /// <param name="command">The command containing the data for publishing the availability.</param>
    public Availability(PublishAvailabilityCommand command)
        : this(command.NutritionistId, new TimeSlot(command.Date, command.StartTime, command.EndTime))
    {
    }

    /// <summary>
    ///     Gets the unique identifier of the availability.
    /// </summary>
    public AvailabilityId Id { get; private set; }

    /// <summary>
    ///     Gets the nutritionist who published the availability.
    /// </summary>
    public NutritionistId NutritionistId { get; private set; }

    /// <summary>
    ///     Gets the day of the availability.
    /// </summary>
    public DateOnly Date { get; private set; }

    /// <summary>
    ///     Gets the time at which the availability starts.
    /// </summary>
    public TimeOnly StartTime { get; private set; }

    /// <summary>
    ///     Gets the time at which the availability ends.
    /// </summary>
    public TimeOnly EndTime { get; private set; }

    /// <summary>
    ///     Gets a value indicating whether the slot is still free. It is also the concurrency token that prevents
    ///     the same slot from being assigned to two patients.
    /// </summary>
    public bool IsAvailable { get; private set; }

    /// <summary>
    ///     Gets the time slot of the availability as a value object.
    /// </summary>
    public TimeSlot Slot => new(Date, StartTime, EndTime);

    /// <summary>
    ///     Marks the slot as taken by an appointment.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the slot is already taken.</exception>
    public void Reserve()
    {
        if (!IsAvailable)
            throw new InvalidOperationException("The availability is already reserved.");
        IsAvailable = false;
    }

    /// <summary>
    ///     Frees the slot so that it can be booked again (the appointment was cancelled or rescheduled).
    /// </summary>
    public void Release()
    {
        IsAvailable = true;
    }
}
