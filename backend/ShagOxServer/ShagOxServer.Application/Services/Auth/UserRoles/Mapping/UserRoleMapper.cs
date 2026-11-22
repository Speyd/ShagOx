using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Domain.Entities.Auth;

namespace ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
public static class UserRoleMapper
{
    public static UserRoleDto ToDto(
        UserRole userRole)
    {
        return new UserRoleDto(
            userRole.Id,
            userRole.EntityStatus,
            userRole.UserId,
            userRole.RoleId
        );
    }
}
