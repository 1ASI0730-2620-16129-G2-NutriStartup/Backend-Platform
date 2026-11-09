using System;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.ValueObjects;

public record Measurements
{
    public decimal WeightKg { get; }
    public decimal HeightMeters { get; }
    public decimal Bmi => Math.Round(WeightKg / (HeightMeters * HeightMeters), 2);

    public Measurements(decimal weightKg, decimal heightMeters)
    {
        if (weightKg is <= 0 or > 999.99m)
            throw new ArgumentOutOfRangeException(nameof(weightKg), "Weight must be in (0, 999.99] kg.");
        if (heightMeters is <= 0 or > 9.99m)
            throw new ArgumentOutOfRangeException(nameof(heightMeters), "Height must be in (0, 9.99] meters.");
        WeightKg = weightKg;
        HeightMeters = heightMeters;
    }
}