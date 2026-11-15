namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record UpdatePreferenceCommand(int PreferenceId, string UserId, string DietaryPreferences, string FavoriteFoods);
