using BackendPlatform.API.Shared.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

public readonly record struct NutritionPlanId(int Value) : IEntityId<int>
{
    public static implicit operator int(NutritionPlanId id) => id.Value;
    public static implicit operator NutritionPlanId(int value) => new(value);
    
    public override string ToString() => Value.ToString();
}
