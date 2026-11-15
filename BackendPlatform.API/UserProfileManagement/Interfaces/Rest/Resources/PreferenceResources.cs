namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

public sealed record PreferenceResource(int Id, string UserId, string DietaryPreferences, string FavoriteFoods);
public sealed record CreatePreferenceResource(string UserId, string DietaryPreferences, string FavoriteFoods);
public sealed record UpdatePreferenceResource(string UserId, string DietaryPreferences, string FavoriteFoods);
