using ShagOxServer.Application.DTOs.Comments.ReviewComments.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
public class ReviewCommentInvalidationService 
    : ICacheInvalidationService<ReviewComment, ReviewCommentCacheInfo>
{
    private readonly IReviewImageQueryRepository _imageRepository;
    private readonly ReviewImageInvalidationService _imageInvalid;

    private readonly IDataCacheService _cache;


    public ReviewCommentInvalidationService(
        IReviewImageQueryRepository imageRepository,
        ReviewImageInvalidationService imageInvalid,
        IDataCacheService cache)
    {
        _imageRepository = imageRepository;
        _imageInvalid = imageInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        ReviewCommentCacheInfo entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        ReviewCommentCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var imageInfos = await GetImageInfos(
            entityInfo.Id);

        foreach(var imageInfo in imageInfos)
        {
            await _imageInvalid
                .InvalidateDeleteAsync(imageInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        ReviewCommentCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        ReviewCommentCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys.
             Entity<ReviewComment>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(ReviewCommentCache.
             ByAdvertisementPattern(entityInfo.AdvertisementId));
    }

    private async Task<List<PictureCacheInfo>> GetImageInfos(
        long entityId)
    {
        return await _imageRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }
}
