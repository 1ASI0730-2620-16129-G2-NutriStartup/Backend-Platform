namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public sealed record CreatePreferenceCommand(string UserId, string DietaryPreferences, string FavoriteFoods);