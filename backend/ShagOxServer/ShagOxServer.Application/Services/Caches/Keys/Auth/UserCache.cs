using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Account;

namespace ShagOxServer.Application.Services.Caches.Keys.Auth;
public static class UserCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<User>();


    public static string ByContact(
        string contact,
        string language,
        EntityStatus status = EntityStatus.Active)
            => $"{Prefix}:{language}:status:{status}:contact:{contact}";

    public static string ByContactPattern(
        string contact,
        EntityStatus status)
            => $"{Prefix}:*:status:{status}:contact:{contact}";

    public static string ByContactPattern(
        string contact)
            => $"{Prefix}:*:contact:{contact}";
}
