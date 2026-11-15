using BackendPlatform.API.Shared.Domain.Model;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;

public class Goal : IAuditableEntity
{
    private Goal()
    {
        Type = string.Empty;
        Status = string.Empty;
    }

    internal Goal(int patientProfileId, string type, decimal targetWeight, DateOnly targetDate, string status)
    {
        if (patientProfileId <= 0)
            throw new ArgumentOutOfRangeException(nameof(patientProfileId));
        
        PatientProfileId = patientProfileId;
        
        Update(type, targetWeight, targetDate, status);
    }

    public int Id { get; private set; }
    public int PatientProfileId { get; private set; }
    public string Type { get; private set; } = string.Empty;
    public decimal TargetWeight { get; private set; }
    public DateOnly TargetDate { get; private set; }
    public string Status { get; private set; } = string.Empty;
    public PatientProfile? PatientProfile { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    internal void Update(string type, decimal targetWeight, DateOnly targetDate, string status)
    {
        Type = Required(type, nameof(type), 80);
        
        if (targetWeight <= 0 || targetWeight > 999.99m)
            throw new ArgumentOutOfRangeException(nameof(targetWeight), "Target weight must be greater than 0 and at most 999.99 kg.");
        
        if (targetDate == default)
            throw new ArgumentException("A target date is required.", nameof(targetDate));
        
        TargetWeight = targetWeight;
        
        TargetDate = targetDate;
        
        Status = Required(status, nameof(status), 40);
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
