using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
public class ReviewImageInvalidationService
    : ICacheInvalidationService<ReviewImage,
        PictureCacheInfo>
{
    private readonly IDataCacheService _cache;


    public ReviewImageInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        PictureCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        PictureCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        PictureCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        PictureCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<ReviewImage>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<ReviewImage>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<ReviewImage>());
    }
}
