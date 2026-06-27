using ShagOxServer.Application.DTOs.Roles;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Roles.Mapping;
public static class RoleMapper
{
    public static RoleDto ToDto(Role role)
    {
        return new RoleDto(
            role.Id,
            role.Name,
            role.Description
        );
    }
}