namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public record CreateRestrictionCommand(string UserId, string Type, string Description);