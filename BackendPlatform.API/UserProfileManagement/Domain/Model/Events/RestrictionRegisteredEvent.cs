using BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

public sealed record RestrictionRegisteredEvent(string UserId, string RestrictionType, DateTimeOffset OccurredOn) : IUserProfileDomainEvent;