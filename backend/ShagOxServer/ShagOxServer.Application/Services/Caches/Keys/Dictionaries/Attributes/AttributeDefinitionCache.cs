using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
public class AttributeDefinitionCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<AttributeDefinition>();
}