using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class ImageInvalidationService
    : ICacheInvalidationService<Image, ImageCacheInfo>
{
    private readonly ICacheService _cache;


    public ImageInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        ImageCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        ImageCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        ImageCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        ImageCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<Image>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<Image>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<Image>());
    }
}