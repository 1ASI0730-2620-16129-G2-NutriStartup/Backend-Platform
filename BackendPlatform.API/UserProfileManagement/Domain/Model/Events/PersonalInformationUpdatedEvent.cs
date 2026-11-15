using BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

public sealed record PersonalInformationUpdatedEvent(string UserId, DateTimeOffset OccurredOn):IUserProfileDomainEvent;