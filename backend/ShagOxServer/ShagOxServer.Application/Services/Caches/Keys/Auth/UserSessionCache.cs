using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserSessionCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<UserSession>();

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
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:user:{userId}";

    public static string AllByUserPattern(
        long userId)
            => $"{Prefix}:*:user:{userId}";




    public static string ByUserDevice(
       long deviceId,
       int page,
       int pageSize,
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:device:{deviceId}:page:{page}:size:{pageSize}";

    public static string ByUserDevicePattern(
        long deviceId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:device:{deviceId}:page:*";

    public static string ByUserDevicePattern(
        long deviceId)
            => $"{Prefix}:*:device:{deviceId}:page:*";



    public static string AllByUserDevice(
      long deviceId,
      EntityStatus status = EntityStatus.Active)
          => $"{Prefix}:status:{status}:device:{deviceId}";

    public static string AllByUserDevicePattern(
        long deviceId)
            => $"{Prefix}:*:device:{deviceId}";



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
}
