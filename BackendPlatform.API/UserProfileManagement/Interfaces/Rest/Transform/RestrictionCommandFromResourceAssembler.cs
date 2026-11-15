using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class RestrictionCommandFromResourceAssembler
{
    public static CreateRestrictionCommand ToCommandFromResource(CreateRestrictionResource resource) =>
        new(resource.UserId, resource.Type, resource.Description);

    public static UpdateRestrictionCommand ToCommandFromResource(UpdateRestrictionResource resource, int id) =>
        new(id, resource.UserId, resource.Type, resource.Description);
}
