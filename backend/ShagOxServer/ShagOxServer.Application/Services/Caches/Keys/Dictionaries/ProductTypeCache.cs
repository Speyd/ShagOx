using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
public class ProductTypeCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<ProductType>();
}