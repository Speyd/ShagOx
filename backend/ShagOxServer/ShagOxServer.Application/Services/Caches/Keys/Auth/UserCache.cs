using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<User>();


    public static string ByContact(
        string contact,
        string language)
            => $"{Prefix}:{language}:contact:{contact}";

    public static string ByContactPattern(
        string contact)
            => $"{Prefix}:*contact:{contact}";
}