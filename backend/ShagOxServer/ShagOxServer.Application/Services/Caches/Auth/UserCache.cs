using ShagOxServer.Domain.Caches;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Auth;
public class UserCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<User>();


    public static string ByContact(
        string contact)
            => $"{Prefix}:contact:{contact}";

    public static string ByContactPattern(
        long userId)
            => $"{Prefix}:contact:*";
}