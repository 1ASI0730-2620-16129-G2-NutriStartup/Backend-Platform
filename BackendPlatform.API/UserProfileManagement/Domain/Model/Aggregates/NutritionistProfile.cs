using System.ComponentModel.DataAnnotations.Schema;
using BackendPlatform.API.Shared.Domain.Model;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

public class NutritionistProfile : IAuditableEntity
{
    private readonly List<IUserProfileDomainEvent> _domainEvents = [];
    private NutritionistProfile()
    {
        UserId = string.Empty;
        Specialty = string.Empty;
    }

    public NutritionistProfile(string userId, string specialty, int experienceYears)
    {
        UserId = ValidateText(userId, nameof(userId), 120);
        UpdateProfessionalInformation(specialty, experienceYears);
    }

    public int Id { get; private set; }
    public string UserId { get; private set; }
    public string Specialty { get; private set; } = string.Empty;
    public int ExperienceYears { get; private set; }
    [NotMapped]
    public IReadOnlyCollection<IUserProfileDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void UpdateProfessionalInformation(string specialty, int experienceYears)
    {
        Specialty = ValidateText(specialty, nameof(specialty), 120);
        if (experienceYears is < 0 or > 80)
            throw new ArgumentOutOfRangeException(nameof(experienceYears), "Experience years must be between 0 and 80.");
        ExperienceYears = experienceYears;
        _domainEvents.Add(new NutritionistProfileUpdatedEvent(UserId, DateTimeOffset.UtcNow));
    }

    private static string ValidateText(string value, string parameterName, int maxLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        var normalized = value.Trim();
        if (normalized.Length > maxLength)
            throw new ArgumentOutOfRangeException(parameterName, $"The value cannot exceed {maxLength} characters.");
        return normalized;
    }
}
