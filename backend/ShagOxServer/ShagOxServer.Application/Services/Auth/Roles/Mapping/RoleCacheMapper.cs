using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Roles.Mapping;
public static class RoleCacheMapper
{
    public static RoleCacheInfo ToInfo(
        Role role)
    {
        return new RoleCacheInfo(
            role.Id,
            role.Name
        );
    }
}