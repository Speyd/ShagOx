using ShagOxServer.Application.DTOs.Advertisements.Statuses.Cache;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.Statuses.Mapping;
public static class StatusCacheMapper
{
    public static StatusCacheInfo ToInfo(
        Status x,
        EntityStatus status = EntityStatus.Active)
    {
        return new StatusCacheInfo
        (
            x.Id,
            status
        );
    }
}
