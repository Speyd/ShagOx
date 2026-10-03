using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Auth;
public class RoleCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Role>();


    public static string ByUser(
        long userId,
        int page,
        int pageSize)
            => $"{Prefix}:user:{userId}:page:{page}:size:{pageSize}";

    public static string ByUserPattern(
        long userId)
            => $"{Prefix}:user:{userId}:page:*";

    public static string ByName(
        string name)
            => $"{Prefix}:name:{name}";
}