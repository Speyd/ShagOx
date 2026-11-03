using ShagOxServer.Application.DTOs.Auth.Roles.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Auth.Roles.Mapping;
public static class RoleCacheMapper
{
    public static RoleCacheInfo ToInfo(
        Role role,
        EntityStatus status = EntityStatus.Active)
    {
        return new RoleCacheInfo(
            role.Id,
            role.Name,
            status
        );
    }
}
