using ShagOxServer.Domain.Base;
using ShagOxServer.Domain.Base.Cache;
using ShagOxServer.Domain.Cache.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ITranslationCacheInvalidationService<TTranslation>
    : ICacheInvalidationService<TTranslation, TranslationCacheContext>
    where TTranslation : BaseEntity, IEntityCache<TTranslation, TranslationCacheContext>
{
}
