using Acme.Center.Platform.Shared.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a patient. The patient lives in another bounded context, so only its identity is referenced here in the Appointment Management bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct PatientId(int Value) : IEntityId<int>
{
    public static implicit operator int(PatientId id) => id.Value;
    public static implicit operator PatientId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
