namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record CreateRestrictionCommand(string UserId, string Type, string Description);