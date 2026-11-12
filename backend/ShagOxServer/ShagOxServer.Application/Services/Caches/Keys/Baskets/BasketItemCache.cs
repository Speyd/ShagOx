using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Baskets;

namespace ShagOxServer.Application.Services.Caches.Keys.Baskets;
public static class BasketItemCache
{
    private static readonly string Prefix =
        CacheKeys.LanguagePrefix<BasketItem>();

}
