using BackendPlatform.API.Shared.Domain.Model;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;

public class Preference : IAuditableEntity
{
    private Preference()
    {
        DietaryPreferences = string.Empty;
        FavoriteFoods = string.Empty;
    }

    internal Preference(int patientProfileId, string dietaryPreferences, string favoriteFoods)
    {
        if (patientProfileId <= 0)
            throw new ArgumentOutOfRangeException(nameof(patientProfileId));
        
        PatientProfileId = patientProfileId;
        
        Update(dietaryPreferences, favoriteFoods);
    }

    public int Id { get; private set; }
    public int PatientProfileId { get; private set; }
    public string DietaryPreferences { get; private set; }
    public string FavoriteFoods { get; private set; }
    public PatientProfile? PatientProfile { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    internal void Update(string dietaryPreferences, string favoriteFoods)
    {
        DietaryPreferences = Normalize(dietaryPreferences, nameof(dietaryPreferences), 500);
        FavoriteFoods = Normalize(favoriteFoods, nameof(favoriteFoods), 500);
        
        if (string.IsNullOrWhiteSpace(DietaryPreferences) && string.IsNullOrWhiteSpace(FavoriteFoods))
            throw new ArgumentException("At least one dietary preference or favorite food must be provided.");
    }

    private static string Normalize(string? value, string parameterName, int maxLength)
    {
        var normalized = value?.Trim() ?? string.Empty;
        
        if (normalized.Length > maxLength)
            throw new ArgumentOutOfRangeException(parameterName, $"The value cannot exceed {maxLength} characters.");
        
        return normalized;
    }
}