using System.ComponentModel.DataAnnotations.Schema;
using BackendPlatform.API.Shared.Domain.Model;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Domain.Model.ValueObjects;
using BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Aggregates;

public class PatientProfile : IAuditableEntity
{
    private readonly List<Goal> _goals = [];
    private readonly List<Preference> _preferences = [];
    private readonly List<Restriction> _restrictions = [];
    private readonly List<IUserProfileDomainEvent> _domainEvents = [];
    
    private PatientProfile()
    {
        UserId = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        Gender = string.Empty;
        Measurements = new Measurements(1m, 1m);
    }

    public PatientProfile(string userId, string firstName, string lastName, int age, string gender,
        decimal weight, decimal height)
    {
        UserId = Required(userId, nameof(userId), 120);
        FirstName = Required(firstName, nameof(firstName), 80);
        LastName = Required(lastName, nameof(lastName), 80);
        Age = ValidateAge(age);
        Gender = Required(gender, nameof(gender), 30);
        Measurements = new Measurements(weight, height);
        
        AddDomainEvent(new PatientProfileCreatedEvent(UserId, DateTimeOffset.UtcNow));
    }

    public int Id { get; private set; }
    public string UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int Age { get; private set; }
    public string Gender { get; private set; }
    public Measurements Measurements { get; private set; }

    [NotMapped]
    public decimal Weight => Measurements.WeightKg;

    [NotMapped]
    public decimal Height => Measurements.HeightMeters;

    [NotMapped]
    public decimal Bmi => Measurements.Bmi;

    public IReadOnlyCollection<Goal> Goals => _goals.AsReadOnly();
    public IReadOnlyCollection<Preference> Preferences => _preferences.AsReadOnly();
    public IReadOnlyCollection<Restriction> Restrictions => _restrictions.AsReadOnly();

    [NotMapped]
    public IReadOnlyCollection<IUserProfileDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    public void UpdatePersonalInformation(string firstName, string lastName, int age, string gender,
        decimal weight, decimal height)
    {
        FirstName = Required(firstName, nameof(firstName), 80);
        LastName = Required(lastName, nameof(lastName), 80);
        Age = ValidateAge(age);
        Gender = Required(gender, nameof(gender), 30);
        Measurements = new Measurements(weight, height);
        
        AddDomainEvent(new PersonalInformationUpdatedEvent(UserId, DateTimeOffset.UtcNow));
    }

    public Goal AddGoal(string type, decimal targetWeight, DateOnly targetDate, string status)
    {
        var goal = new Goal(Id, type, targetWeight, targetDate, status);
        
        _goals.Add(goal);
        
        AddDomainEvent(new NutritionalGoalRegisteredEvent(UserId, goal.Type, DateTimeOffset.UtcNow));
        
        return goal;
    }
    
    public Goal UpdateGoal(int goalId, string type, decimal targetWeight, DateOnly targetDate, string status)
    {
        var goal = _goals.FirstOrDefault(item => item.Id == goalId)
                   ?? throw new KeyNotFoundException($"Goal {goalId} was not found in this patient profile.");
        
        goal.Update(type, targetWeight, targetDate, status);
        
        return goal;
    }

    public bool RemoveGoal(int goalId)
    {
        var goal = _goals.FirstOrDefault(item => item.Id == goalId);
        
        return goal is not null && _goals.Remove(goal);
    }

    public Preference AddPreference(string dietaryPreferences, string favoriteFoods)
    {
        var preference = new Preference(Id, dietaryPreferences, favoriteFoods);
        
        _preferences.Add(preference);
        
        AddDomainEvent(new PreferencesUpdatedEvent(UserId, DateTimeOffset.UtcNow));
        
        return preference;
    }

    public Preference UpdatePreference(int preferenceId, string dietaryPreferences, string favoriteFoods)
    {
        var preference = _preferences.FirstOrDefault(item => item.Id == preferenceId)
                         ?? throw new KeyNotFoundException($"Preference {preferenceId} was not found in this patient profile.");
        
        preference.Update(dietaryPreferences, favoriteFoods);
        
        return preference;
    }

    public bool RemovePreference(int preferenceId)
    {
        var preference = _preferences.FirstOrDefault(item => item.Id == preferenceId);
        
        return preference is not null && _preferences.Remove(preference);
    }

    public Restriction AddRestriction(string type, string description)
    {
        var restriction = new Restriction(Id, type, description);
        
        _restrictions.Add(restriction);
        
        AddDomainEvent(new RestrictionRegisteredEvent(UserId, restriction.Type, DateTimeOffset.UtcNow));
        
        return restriction;
    }

    public Restriction UpdateRestriction(int restrictionId, string type, string description)
    {
        var restriction = _restrictions.FirstOrDefault(item => item.Id == restrictionId)
                          ?? throw new KeyNotFoundException($"Restriction {restrictionId} was not found in this patient profile.");
        
        restriction.Update(type, description);
        
        return restriction;
    }

    public bool RemoveRestriction(int restrictionId)
    {
        var restriction = _restrictions.FirstOrDefault(item => item.Id == restrictionId);
        
        return restriction is not null && _restrictions.Remove(restriction);
    }

    private void AddDomainEvent(IUserProfileDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    private static string Required(string value, string parameterName, int maxLength)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);
        
        var normalized = value.Trim();
        
        if (normalized.Length > maxLength)
            throw new ArgumentOutOfRangeException(parameterName, $"The value cannot exceed {maxLength} characters.");
        
        return normalized;
    }

    private static int ValidateAge(int age)
    {
        if (age is < 1 or > 120)
            throw new ArgumentOutOfRangeException(nameof(age), "Age must be between 1 and 120.");
        
        return age;
    }
}
