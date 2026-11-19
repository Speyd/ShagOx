using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class CategoryImageInvalidationService
    : ICacheInvalidationService<CategoryImage, PictureCacheInfo>
{
    private readonly IDataCacheService _cache;


    public CategoryImageInvalidationService(
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
            .Entity<CategoryImage>(entityInfo.Id));

        await _cache.RemoveAsync(CategoryImageCache
           .ByCategory(entityInfo.OwnerId));
    }
}
