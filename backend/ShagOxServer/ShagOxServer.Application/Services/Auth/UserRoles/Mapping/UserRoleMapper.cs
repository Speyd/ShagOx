using ShagOxServer.Application.DTOs.Auth.UserRoles;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.UserRoles.Mapping;
public static class UserRoleMapper
{
    public static UserRoleDto ToDto(
        UserRole userRole)
    {
        return new UserRoleDto(
            userRole.Id,
            userRole.UserId,
            userRole.RoleId
        );
    }
}