using BackendPlatform.API.Shared.Domain.Model.ValueObjects;

namespace BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;

public readonly record struct FoodRecommendationId(int Value) : IEntityId<int>
{
    public static implicit operator int(FoodRecommendationId id) => id.Value;
    public static implicit operator FoodRecommendationId(int value) => new(value);
    
    public override string ToString() => Value.ToString();
}
