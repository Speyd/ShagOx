using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
public class ReviewRepliImageInvalidationService
    : ICacheInvalidationService<ReviewRepliImage,
        PictureCacheContext>
{
    private readonly IDataCacheService _cache;


    public ReviewRepliImageInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        PictureCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        PictureCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        PictureCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        PictureCacheContext entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<ReviewRepliImage>(entityInfo.Id));
    }
}
