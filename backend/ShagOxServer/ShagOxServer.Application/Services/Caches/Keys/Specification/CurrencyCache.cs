using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification;
public static class CurrencyCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<Currency>();


    public static string ByCode(
       string code)
           => $"{Prefix}:code:{code}";

    public static string BySymbol(
       long symbol)
           => $"{Prefix}:symbol:{symbol}";
}