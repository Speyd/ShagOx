using ShagOxServer.Application.DTOs.Auth.Roles.Create;
using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Application.Services.Auth.Roles.Create;
public static class RoleCreater
{
    public static Role Create(
       RoleCreateRequest request)
    {
        return new Role
        {
            Name = request.Name,
            Description = request.Description ?? "",
        };
    }
}
