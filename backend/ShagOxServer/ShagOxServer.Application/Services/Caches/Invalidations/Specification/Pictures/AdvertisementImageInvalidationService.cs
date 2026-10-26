using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Advertisements;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class AdvertisementImageInvalidationService
    : ICacheInvalidationService<AdvertisementImage, 
        PictureCacheInfo>
{
    private readonly IDataCacheService _cache;

    private readonly IAdvertisementQueryRepository _ownerRepository;
    private readonly AdvertisementInvalidationService _ownerInvalid;


    public AdvertisementImageInvalidationService(
        IDataCacheService cache,
        IAdvertisementQueryRepository ownerRepository,
        AdvertisementInvalidationService ownerInvalid)
    {
        _cache = cache;
        _ownerRepository = ownerRepository;
        _ownerInvalid = ownerInvalid;
    }


    public async Task InvalidateCreateAsync(
        PictureCacheInfo entityInfo)
    {
        await InvalidOwner(entityInfo);
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
        await InvalidOwner(entityInfo);

        await _cache.RemoveAsync(CacheKeys
            .Entity<AdvertisementImage>(entityInfo.Id));
    }

    private async Task InvalidOwner(
        PictureCacheInfo entityInfo)
    {
        var info = await _ownerRepository
            .GetCacheInfoByIdAsync(entityInfo.OwnerId);

        if (info is null)
            return;

        await _ownerInvalid
            .InvalidateUpdateAsync(info);
    }
}
