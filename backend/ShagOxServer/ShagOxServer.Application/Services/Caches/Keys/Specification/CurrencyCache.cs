using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification;
public static class CurrencyCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Currency>();


    public static string ByCode(
       string code,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:code:{code}";
    public static string ByCodePattern(
      string code)
          => $"{Prefix}:*:code:{code}";

    public static string BySymbol(
       string symbol,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:symbol:{symbol}";

    public static string BySymbolPattern(
       string symbol)
           => $"{Prefix}:*:symbol:{symbol}";

    public static string ByUser(
       long userId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status:{status}:userId:{userId}";

    public static string ByUserPattern(
       long userId)
           => $"{Prefix}:*:userId:{userId}";
}
