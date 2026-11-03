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
           => $"{Prefix}:status{status}:code:{code}";

    public static string BySymbol(
       string symbol,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:status{status}:symbol:{symbol}";
}
