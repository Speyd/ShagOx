using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
public class ReviewRepliImageInvalidationService
    : ICacheInvalidationService<ReviewRepliImage,
        PictureCacheInfo>
{
    private readonly IDataCacheService _cache;
    private readonly IReviewRepliCommentQueryRepository _ownerRepository;
    //private readonly ReviewRepliCommentInvalidationService _ownerInvalid;


    public ReviewRepliImageInvalidationService(
        IDataCacheService cache,
        IReviewRepliCommentQueryRepository ownerRepository)
    {
        _cache = cache;
        _ownerRepository = ownerRepository;
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
            .Entity<ReviewRepliImage>(entityInfo.Id));
    }

    private async Task InvalidOwner(
       PictureCacheInfo entityInfo)
    {
        var info = await _ownerRepository
            .GetCacheInfoByIdAsync(entityInfo.OwnerId);

        if (info is null)
            return;

        // await _ownerInvalid
        //.InvalidateUpdateAsync(info);
    }
}
