using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
public class AttributeDictionaryValueCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<AttributeDictionaryValue>();

    public static string ByDictionary(
       long dictionaryId,
       string language,
       int page,
       int pageSize)
           => $"{Prefix}:{language}:dictionary:{dictionaryId}:page:{page}:size:{pageSize}";

    public static string ByDictionaryPattern(
       long dictionaryId)
           => $"{Prefix}:*dictionary:{dictionaryId}:page:*";
}