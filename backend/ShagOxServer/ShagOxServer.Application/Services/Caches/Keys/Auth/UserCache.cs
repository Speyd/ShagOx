using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public class UserCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<User>();


    public static string ByContact(
        string contact)
            => $"{Prefix}:contact:{contact}";
}