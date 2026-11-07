using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
public class ReviewRepliImageInvalidationService
    : ICacheInvalidationService<ReviewRepliImage,
        PictureCacheInfo>
{
    private readonly IDataCacheService _cache;


    public ReviewRepliImageInvalidationService(
        IDataCacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        PictureCacheInfo entityInfo)
    {
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
            .Entity<ReviewRepliImage>(entityInfo.Id));
    }
}
