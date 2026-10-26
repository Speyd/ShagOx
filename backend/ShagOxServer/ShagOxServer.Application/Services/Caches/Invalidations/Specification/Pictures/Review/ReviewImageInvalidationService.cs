using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
public class ReviewImageInvalidationService
    : ICacheInvalidationService<ReviewImage,
        PictureCacheInfo>
{
    private readonly IDataCacheService _cache;
    private readonly IReviewCommentQueryRepository _ownerRepository;
    private readonly ReviewCommentInvalidationService _ownerInvalid;


    public ReviewImageInvalidationService(
        IDataCacheService cache,
        IReviewCommentQueryRepository ownerRepository,
        ReviewCommentInvalidationService ownerInvalid)
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
            .Entity<ReviewImage>(entityInfo.Id));
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
