using ShagOxServer.Application.DTOs.Specification.Pictures.Avatars.Cache;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class AvatarInvalidationService
    : ICacheInvalidationService<Condition, AvatarCacheInfo>
{
    private readonly ICacheService _cache;


    public AvatarInvalidationService(
        ICacheService cache)
    {
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        AvatarCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateDeleteAsync(
        AvatarCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    public async Task InvalidateUpdateAsync(
        AvatarCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        AvatarCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys
            .Entity<Avatar>(entityInfo.Id));

        await _cache.RemoveAsync(AvatarCache
           .ByUser(entityInfo.UserId));

        await _cache.RemoveByPatternAsync(CacheKeys.
           EntityPagedPattern<Avatar>());

        await _cache.RemoveByPatternAsync(CacheKeys.
             SearchPattern<Avatar>());
    }
}