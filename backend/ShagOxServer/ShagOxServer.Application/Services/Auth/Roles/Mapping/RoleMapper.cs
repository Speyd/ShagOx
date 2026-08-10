using ShagOxServer.Application.DTOs.Auth.Roles;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Roles.Mapping;
public static class RoleMapper
{
    public static RoleDto ToDto(
        Role role)
    {
        return new RoleDto(
            role.Id,
            role.Name,
            role.Description
        );
    }
}