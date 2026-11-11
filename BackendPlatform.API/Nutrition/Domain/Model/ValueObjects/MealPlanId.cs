using BackendPlatform.API.Shared.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

public record MealPlanId(int Value) : IEntityId<int>
{
    public static implicit operator int(MealPlanId id) => id.Value;
    public static implicit operator MealPlanId(int value) => new(value);
    
    public override string ToString() => Value.ToString();
}
