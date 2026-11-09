namespace BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;

public record CreatePreferenceCommand(string UserId, string DietPreferences, string FavoriteFoods);