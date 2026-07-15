using ShagOxServer.Application.DTOs.Roles.Create;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Roles.Create;
public static class RoleCreater
{
    public static Role CreateRole(
       RoleCreateRequest request)
    {
        return new Role
        {
            Name = request.Name,
            Description = request.Description ?? "",
        };
    }
}