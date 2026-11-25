using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
using ShagOxServer.Domain.Cache.Comments;
using ShagOxServer.Domain.Cache.Specification;
using ShagOxServer.Domain.Entities.Comments.Review;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
public class ReviewCommentInvalidationService 
    : ICacheInvalidationService<ReviewComment, CommentCacheContext>
{
    private readonly IReviewImageQueryRepository _imageRepository;
    private readonly ReviewImageInvalidationService _imageInvalid;

    private readonly IReviewRepliCommentQueryRepository _repliRepository;
    private readonly ReviewRepliCommentInvalidationService _repliInvalid;

    private readonly IDataCacheService _cache;


    public ReviewCommentInvalidationService(
        IReviewImageQueryRepository imageRepository,
        ReviewImageInvalidationService imageInvalid,
        IReviewRepliCommentQueryRepository repliRepository,
        ReviewRepliCommentInvalidationService repliInvalid,
        IDataCacheService cache)
    {
        _imageRepository = imageRepository;
        _imageInvalid = imageInvalid;
        _repliRepository = repliRepository;
        _repliInvalid = repliInvalid;
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

        foreach(var imageInfo in imageInfos)
        {
            await _imageInvalid
                .InvalidateDeleteAsync(imageInfo);
        }


        var repliInfos = await GetRepliInfos(
            entityInfo.Id);

        foreach (var repliInfo in repliInfos)
        {
            await _repliInvalid
                .InvalidateDeleteAsync(repliInfo);
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
             Entity<ReviewComment>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(ReviewCommentCache.
             ByAdvertisementPattern(entityInfo.CommentableEntityId));
    }

    private async Task<List<PictureCacheContext>> GetImageInfos(
        long entityId)
    {
        return await _imageRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }

    private async Task<List<CommentCacheContext>> GetRepliInfos(
        long entityId)
    {
        return await _repliRepository
            .GetCacheInfosByCommentableAsync(entityId);
    }
}
