using BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

public sealed record PreferencesUpdatedEvent(string UserId, DateTimeOffset OccurredOn) : IUserProfileDomainEvent;