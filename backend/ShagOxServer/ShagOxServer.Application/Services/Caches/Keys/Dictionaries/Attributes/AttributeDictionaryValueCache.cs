using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
public static class AttributeDictionaryValueCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<AttributeDictionaryValue>();

    public static string ByDictionary(
       long dictionaryId,
       string language,
       int page,
       int pageSize,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:{language}:status{status}:dictionary:{dictionaryId}:page:{page}:size:{pageSize}";

    public static string ByDictionaryPattern(
       long dictionaryId,
        EntityStatus status = EntityStatus.Active)
           => $"{Prefix}:*status{status}:dictionary:{dictionaryId}:page:*";
}
