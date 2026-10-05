using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Services.Caches.Keys.Specification;
public static class ConditionCache
{
    public static readonly string Prefix =
       CacheKeys.LanguagePrefix<Condition>();
}