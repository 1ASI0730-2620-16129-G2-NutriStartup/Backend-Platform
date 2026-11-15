using BackendPlatform.API.Shared.Domain.Model;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;

public class Restriction : IAuditableEntity
{
    private Restriction()
    {
        Type = string.Empty;
        Description = string.Empty;
    }

    internal Restriction(int patientProfileId, string type, string description)
    {
        if (patientProfileId <= 0)
            throw new ArgumentOutOfRangeException(nameof(patientProfileId));
        
        PatientProfileId = patientProfileId;
        
        Update(type, description);
    }

    public int Id { get; private set; }
    public int PatientProfileId { get; private set; }
    public string Type { get; private set; }
    public string Description { get; private set; }
    public PatientProfile? PatientProfile { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    internal void Update(string type, string description)
    {
        Type = Required(type, nameof(type), 80);
        
        Description = Required(description, nameof(description), 500);
    }

    private static string Required(string value, string parameterName, int maxLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        
        var normalized = value.Trim();
        
        if (normalized.Length > maxLength)
            throw new ArgumentOutOfRangeException(parameterName, $"The value cannot exceed {maxLength} characters.");
        
        return normalized;
    }
}