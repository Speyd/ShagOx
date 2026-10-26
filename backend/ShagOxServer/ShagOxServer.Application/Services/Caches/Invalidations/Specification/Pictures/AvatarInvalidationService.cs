using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Auth.Users.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Auth;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Specification.Pictures;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Pictures;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures;
public class AvatarInvalidationService
    : ICacheInvalidationService<Condition, PictureCacheInfo>
{
    private readonly IDataCacheService _cache;

    private readonly IUserQueryRepository _ownerRepository;
    private readonly UserInvalidationService _ownerInvalid;


    public AvatarInvalidationService(
        IDataCacheService cache,
        IUserQueryRepository ownerRepository,
        UserInvalidationService ownerInvalid)
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
            .Entity<Avatar>(entityInfo.Id));

        await _cache.RemoveAsync(AvatarCache
           .ByUser(entityInfo.OwnerId));
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
