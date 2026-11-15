namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record UpdateRestrictionCommand(int RestrictionId, string UserId, string Type, string Description);
