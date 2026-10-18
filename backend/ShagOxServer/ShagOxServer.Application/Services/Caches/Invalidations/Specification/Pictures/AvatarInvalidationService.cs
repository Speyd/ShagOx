using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class AvatarInvalidationService
    : ICacheInvalidationService<Condition, PictureCacheInfo>
{
    private readonly IDataCacheService _cache;


    public AvatarInvalidationService(
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
            .Entity<Avatar>(entityInfo.Id));

        await _cache.RemoveAsync(AvatarCache
           .ByUser(entityInfo.OwnerId));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<Avatar>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<Avatar>());
    }
}
