using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Translations;
public static class ProductTypeTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ProductTypeTranslation>();
}
