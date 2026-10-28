using ShagOxServer.Domain.Entities.Dictionaries.Attributes;

namespace ShagOxServer.Application.Services.Caches.Keys.Dictionaries.Attributes;
public static class AttributeDefinitionCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<AttributeDefinition>();

    public static string ValueByCategory(
      long categoryId,
      string language,
      int page,
      int pageSize)
          => $"{Prefix}:{language}:value-category:{categoryId}:page:{page}:size:{pageSize}";

    public static string ValueByCategoryPattern(
       long categoryId)
           => $"{Prefix}:*value-category:{categoryId}:page:*";
}
