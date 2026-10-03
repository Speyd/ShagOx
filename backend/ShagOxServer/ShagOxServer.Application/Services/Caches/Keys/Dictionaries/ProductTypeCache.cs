using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
public static class ProductTypeCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<ProductType>();
}