using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
public static class AttributeDefinitionCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<AttributeDefinition>();

    public static string ValueByCategory(
     long categoryId,
     string language)
         => $"{Prefix}:{language}:category:{categoryId}";

    public static string ValueByCategoryPattern(
       long categoryId)
           => $"{Prefix}:*category:{categoryId}";
}
