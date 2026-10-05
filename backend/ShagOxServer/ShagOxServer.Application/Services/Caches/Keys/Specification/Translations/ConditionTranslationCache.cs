using ShagOxServer.Domain.Entities.Specification.Translations;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification.Translations;
public static class ConditionTranslationCache
{
    public static readonly string Prefix =
       CacheKeys.Prefix<ConditionTranslation>();
}