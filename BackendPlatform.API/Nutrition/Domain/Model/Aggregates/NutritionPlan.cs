using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace BackendPlatform.API.Nutrition.Domain.Model.Aggregates;

public partial class NutritionPlan : AggregateRoot
{
    public NutritionPlan(string name, string description, string objective,
        string startDate, string endDate)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(objective);
        ArgumentException.ThrowIfNullOrWhiteSpace(startDate);
        ArgumentException.ThrowIfNullOrWhiteSpace(endDate);
        Name = name;
        Description = description;
        Objective = objective;
        StartDate = startDate;
        EndDate = endDate;
        Status = NutritionPlanStatus.Active;
        AddDomainEvent(new NutritionPlanCreatedEvent(Id, name, description, objective, startDate, endDate,
            Status.ToString()));
    }

    public NutritionPlan(CreateNutritionPlanCommand command) : this(command.Name, command.Description,
        command.Objective, command.StartDate, command.EndDate)
    {
        
    }
    
    public NutritionPlanId Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Objective { get; private set; }
    public string StartDate { get; private set; }
    public string EndDate { get; private set; }
    public NutritionPlanStatus Status { get; private set; } = NutritionPlanStatus.Active;
    
    public void UpdateInformation(
        string name,
        string description,
        string objective,
        string startDate,
        string endDate,
        string status)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(objective);
        ArgumentException.ThrowIfNullOrWhiteSpace(startDate);
        ArgumentException.ThrowIfNullOrWhiteSpace(endDate);
        
        var parsedStatus = ParseStatus(status);

        Name = name;
        Description = description;
        Objective = objective;
        StartDate = startDate;
        EndDate = endDate;
        Status = parsedStatus;
    }
    
    private static NutritionPlanStatus ParseStatus(string status)
    {
        if (!Enum.TryParse(status, ignoreCase: true, out NutritionPlanStatus parsedStatus) ||
            !Enum.IsDefined(typeof(NutritionPlanStatus), parsedStatus))
        {
            throw new ArgumentException($"'{status}' is not valid", nameof(status));
        }
        return parsedStatus;
    }
}
