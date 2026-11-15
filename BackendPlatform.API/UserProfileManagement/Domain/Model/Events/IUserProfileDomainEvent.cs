namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Events;

public interface IUserProfileDomainEvent
{
    DateTimeOffset OccurredOn { get; }
}