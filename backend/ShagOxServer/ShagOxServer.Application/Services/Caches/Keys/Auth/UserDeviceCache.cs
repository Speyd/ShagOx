using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserDeviceCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<UserDevice>();


    public static string ByUser(
       long userId,
       int page,
       int pageSize,
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:user:{userId}:page:*";

    public static string ByUserPattern(
        long userId)
            => $"{Prefix}:*:user:{userId}:page:*";


    public static string AllByUser(
        long userId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:user:{userId}";

    public static string AllByUserPattern(
        long userId)
            => $"{Prefix}:*:user:{userId}";


    public static string ByIpAddress(
       string ipAddress,
       int page,
       int pageSize,
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:ipAddress:{ipAddress}:page:{page}:size:{pageSize}";

    public static string ByIpAddressPattern(
        string ipAddress,
        EntityStatus status)
            => $"{Prefix}:status:{status}:ipAddress:{ipAddress}:page:*";

    public static string ByIpAddressPattern(
        string ipAddress)
            => $"{Prefix}:*:ipAddress:{ipAddress}:page:*";


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
