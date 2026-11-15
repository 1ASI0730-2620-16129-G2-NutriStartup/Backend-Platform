using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class PreferenceResourceFromEntityAssembler
{
    public static PreferenceResource ToResourceFromEntity(Preference entity, string userId) =>
        new(entity.Id, userId, entity.DietaryPreferences, entity.FavoriteFoods);
}
