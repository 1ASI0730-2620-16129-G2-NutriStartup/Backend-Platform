using Acme.Center.Platform.Shared.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for an availability (published time slot) in the Appointment Management bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct AvailabilityId(int Value) : IEntityId<int>
{
    public static implicit operator int(AvailabilityId id) => id.Value;
    public static implicit operator AvailabilityId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
