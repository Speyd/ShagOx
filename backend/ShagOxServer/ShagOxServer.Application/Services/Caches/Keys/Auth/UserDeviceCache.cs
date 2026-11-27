using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Auth.Users;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserDeviceCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<UserDevice>();

    public static string ByUserAndDevice(
      long userId,
      long deviceId,
      EntityStatus status = EntityStatus.Active)
          => $"{Prefix}:status:{status}:user:{userId}:device:{deviceId}";

    public static string ByUserAndDevicePattern(
       long userId,
       long deviceId)
            => $"{Prefix}:*:user:{userId}:device:{deviceId}";

    public static string ByUserIdAndDevicePattern(
        long userId)
            => $"{Prefix}:*:user:{userId}:*";

    public static string ByUserAndDeviceIdPattern(
        long deviceId)
            => $"{Prefix}:*:device:{deviceId}";




    public static string EntityByUserAndDevice(
      long userId,
      long deviceId,
      EntityStatus status = EntityStatus.Active)
          => $"{Prefix}:status:{status}:user:{userId}:device:{deviceId}";

    public static string EntityByUserAndDevicePattern(
        long userId,
       long deviceId)
            => $"{Prefix}:*:user:{userId}:device:{deviceId}";

    public static string EntityByUserIdAndDevicePattern(
        long userId)
            => $"{Prefix}:*:user:{userId}:*";

    public static string EntityByUserAndDeviceIdPattern(
        long deviceId)
            => $"{Prefix}:*:device:{deviceId}";






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



    public static string ByDevice(
       long deviceId,
       int page,
       int pageSize,
       EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:device:{deviceId}:page:{page}:size:{pageSize}";

    public static string ByDevicePattern(
        long deviceId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:device:{deviceId}:page:*";

    public static string ByDevicePattern(
        long deviceId)
            => $"{Prefix}:*:device:{deviceId}:page:*";


    public static string AllByDevice(
        long deviceId,
        EntityStatus status)
            => $"{Prefix}:status:{status}:device:{deviceId}";

    public static string AllByDevicePattern(
        long deviceId)
            => $"{Prefix}:*:device:{deviceId}";

}
