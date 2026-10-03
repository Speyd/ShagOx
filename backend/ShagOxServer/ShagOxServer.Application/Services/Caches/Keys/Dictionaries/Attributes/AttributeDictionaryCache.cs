using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
public class AttributeDictionaryCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<AttributeDictionary>();
}