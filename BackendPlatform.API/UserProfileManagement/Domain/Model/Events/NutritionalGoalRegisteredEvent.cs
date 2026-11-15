using BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

public sealed record NutritionalGoalRegisteredEvent(string UserId, string GoalType, DateTimeOffset OccurredOn):IUserProfileDomainEvent;