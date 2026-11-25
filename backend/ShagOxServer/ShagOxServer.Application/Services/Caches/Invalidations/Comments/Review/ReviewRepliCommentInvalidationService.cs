using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
public class ReviewRepliCommentInvalidationService
    : ICacheInvalidationService<ReviewRepliComment, CommentCacheContext>
{
    private readonly IReviewRepliImageQueryRepository _imageRepository;
    private readonly ReviewRepliImageInvalidationService _imageInvalid;

    private readonly IDataCacheService _cache;


    public ReviewRepliCommentInvalidationService(
        IReviewRepliImageQueryRepository imageRepository,
        ReviewRepliImageInvalidationService imageInvalid,
        IDataCacheService cache)
    {
        _imageRepository = imageRepository;
        _imageInvalid = imageInvalid;
        _cache = cache;
    }


    public async Task InvalidateCreateAsync(
        CommentCacheContext entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        CommentCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);


        var imageInfos = await GetImageInfos(
            entityInfo.Id);

        foreach (var imageInfo in imageInfos)
        {
            await _imageInvalid
                .InvalidateDeleteAsync(imageInfo);
        }
    }

    public async Task InvalidateUpdateAsync(
        CommentCacheContext entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        CommentCacheContext entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys.
             Entity<ReviewRepliComment>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(ReviewRepliCommentCache.
             ByReviewCommentPattern(entityInfo.CommentableEntityId));

        await _cache.RemoveByPatternAsync(ReviewRepliCommentCache.
             ByReviewRepliCommentPattern(entityInfo.Id));
    }

    private async Task<List<PictureCacheContext>> GetImageInfos(
        long entityId)
    {
        return await _imageRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }
}
