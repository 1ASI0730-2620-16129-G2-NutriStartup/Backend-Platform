using BackendPlatform.API.Nutrition.Domain.Model.Commands;
using BackendPlatform.API.Nutrition.Domain.Model.Events;
using BackendPlatform.API.Nutrition.Domain.Model.ValueObjects;
using BackendPlatform.API.Shared.Domain.Model.Aggregates.Internal;

namespace BackendPlatform.API.Nutrition.Domain.Model.Aggregates;

public partial class NutritionPlan : AggregateRoot
{
    public NutritionPlan(string name, string description, string objective,
        string startDate, string endDate, string status)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(objective);
        ArgumentException.ThrowIfNullOrWhiteSpace(startDate);
        ArgumentException.ThrowIfNullOrWhiteSpace(endDate);
        ArgumentException.ThrowIfNullOrWhiteSpace(status);
        Name = name;
        Description = description;
        Objective = objective;
        StartDate = startDate;
        EndDate = endDate;
        Status = status;
        AddDomainEvent(new NutritionPlanCreatedEvent(Id, name, description, objective, startDate, endDate, status));
    }

    public NutritionPlan(CreateNutritionPlanCommand command) : this(command.Name, command.Description, command.Objective, command.StartDate, command.EndDate, command.Status)
    {
        
    }
    
    public NutritionPlanId Id { get; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Objective { get; private set; }
    public string StartDate { get; private set; }
    public string EndDate { get; private set; }
    public string Status { get; private set; }
}
