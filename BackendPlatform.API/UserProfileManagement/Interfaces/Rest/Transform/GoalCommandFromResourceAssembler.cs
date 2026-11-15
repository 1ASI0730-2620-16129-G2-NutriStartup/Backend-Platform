using BackendPlatform.API.UserProfileManagement.Domain.Model.Commands;
using BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Resources;

namespace BackendPlatform.API.UserProfileManagement.Interfaces.Rest.Transform;

public static class GoalCommandFromResourceAssembler
{
    public static CreateGoalCommand ToCommandFromResource(CreateGoalResource resource) =>
        new(resource.UserProfileId, resource.Type, resource.TargetWeight, resource.TargetDate, resource.Status);

    public static UpdateGoalCommand ToCommandFromResource(UpdateGoalResource resource, int id) =>
        new(id, resource.UserProfileId, resource.Type, resource.TargetWeight, resource.TargetDate, resource.Status);
}
