using ShagOxServer.Application.DTOs.Base.Cache;
using ShagOxServer.Domain.Base;

namespace ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
public interface ITranslationCacheInvalidationService<TTranslation>
    where TTranslation : BaseEntity
{
    Task InvalidateDeleteAsync(
        BaseTranslationCacheInfo cacheDto);

    Task InvalidateUpdateAsync(
        BaseTranslationCacheInfo cacheDto);
}