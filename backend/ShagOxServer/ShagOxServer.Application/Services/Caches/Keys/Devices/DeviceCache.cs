using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Devices;

namespace ShagOxServer.Application.Services.Caches.Keys.Devices;
public static class DeviceCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Device>();

    public static string ByIdentifier(
       string identifier,
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:identifier:{identifier}";

    public static string ByIdentifierPattern(
        string identifier)
            => $"{Prefix}:*:identifier:{identifier}";


    public static string EntityByIdentifier(
       string identifier,
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:entity:identifier:{identifier}";

    public static string EntityByIdentifierPattern(
        string identifier)
            => $"{Prefix}:*:entity:identifier:{identifier}";
}
