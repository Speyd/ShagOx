using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes.Translations;
public class AttributeDictionaryValueTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<AttributeDictionaryValueTranslation>();
}