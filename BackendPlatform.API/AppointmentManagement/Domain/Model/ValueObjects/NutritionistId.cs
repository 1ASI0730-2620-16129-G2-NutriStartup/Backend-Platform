using Acme.Center.Platform.Shared.Domain.Model.ValueObjects;

namespace Acme.Center.Platform.AppointmentManagement.Domain.Model.ValueObjects;

/// <summary>
///     Represents the strongly-typed identifier for a nutritionist. The nutritionist lives in another bounded context, so only its identity is referenced here in the Appointment Management bounded context.
/// </summary>
/// <param name="Value">The underlying identifier value.</param>
public readonly record struct NutritionistId(int Value) : IEntityId<int>
{
    public static implicit operator int(NutritionistId id) => id.Value;
    public static implicit operator NutritionistId(int value) => new(value);

    public override string ToString() => Value.ToString();
}
