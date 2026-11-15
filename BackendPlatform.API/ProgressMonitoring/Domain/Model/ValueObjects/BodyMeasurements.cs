namespace BackendPlatform.API.ProgressMonitoring.Domain.Model.ValueObjects;

/// <summary>
///     Body measurements captured in a single progress check-in.
///     Only the weight is mandatory; the rest are optional.
/// </summary>
public record BodyMeasurements
{

    private BodyMeasurements()
    {
    }

    public BodyMeasurements(decimal weightKg, decimal? bodyFatPercentage = null, decimal? waistCm = null)
    {
        if (weightKg <= 0 || weightKg > 500)
            throw new ArgumentOutOfRangeException(nameof(weightKg), "Weight must be between 0 and 500 kg.");

        if (bodyFatPercentage is < 0 or > 100)
            throw new ArgumentOutOfRangeException(nameof(bodyFatPercentage),
                "Body fat percentage must be between 0 and 100.");

        if (waistCm is <= 0 or > 300)
            throw new ArgumentOutOfRangeException(nameof(waistCm), "Waist must be between 0 and 300 cm.");

        WeightKg = weightKg;
        BodyFatPercentage = bodyFatPercentage;
        WaistCm = waistCm;
    }

    public decimal WeightKg { get; init; }
    public decimal? BodyFatPercentage { get; init; }
    public decimal? WaistCm { get; init; }
}