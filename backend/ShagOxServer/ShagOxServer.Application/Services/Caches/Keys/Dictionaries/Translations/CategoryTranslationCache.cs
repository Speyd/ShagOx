using ShagOxServer.Domain.Entities.Dictionaries.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Translations;
public class CategoryTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<CategoryTranslation>();
}