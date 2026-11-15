using BackendPlatform.API.UserProfileManagement.Domain.Model.Entities;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class RestrictionResourceFromEntityAssembler
{
    public static RestrictionResource ToResourceFromEntity(Restriction entity, string userId) =>
        new(entity.Id, userId, entity.Type, entity.Description);
}
