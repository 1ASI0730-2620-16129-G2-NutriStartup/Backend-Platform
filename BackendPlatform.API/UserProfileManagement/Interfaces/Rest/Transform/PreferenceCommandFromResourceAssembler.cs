using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class PreferenceCommandFromResourceAssembler
{
    public static CreatePreferenceCommand ToCommandFromResource(CreatePreferenceResource resource) =>
        new(resource.UserId, resource.DietaryPreferences, resource.FavoriteFoods);

    public static UpdatePreferenceCommand ToCommandFromResource(UpdatePreferenceResource resource, int id) =>
        new(id, resource.UserId, resource.DietaryPreferences, resource.FavoriteFoods);
}
