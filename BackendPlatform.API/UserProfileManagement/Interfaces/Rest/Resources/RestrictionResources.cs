namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

public sealed record RestrictionResource(int Id, string UserId, string Type, string Description);
public sealed record CreateRestrictionResource(string UserId, string Type, string Description);
public sealed record UpdateRestrictionResource(string UserId, string Type, string Description);
