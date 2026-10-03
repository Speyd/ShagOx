using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Advertisements;

namespace ShagOxServer.Application.Services.Caches.Advertisements;
public class StatusCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Status>();
}