using ShagOxServer.Domain.Entities.Dictionaries.Attributes.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes.Translations;
public static class AttributeDefinitionTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<AttributeDefinitionTranslation>();
}