using ShagOxServer.Application.DTOs.Comments.Core.Cache;
using ShagOxServer.Application.DTOs.Specification.Pictures.Core.Cache;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Caches.Invalidations;
using ShagOxServer.Application.Services.Caches.Invalidations.Specification.Pictures.Review;
using ShagOxServer.Application.Services.Caches.Keys;
using ShagOxServer.Application.Services.Caches.Keys.Comments.Review;
using ShagOxServer.Domain.Entities.Comments;

namespace ShagOxServer.Application.Services.Caches.Invalidations.Comments.Review;
public class ReviewRepliCommentInvalidationService
    : ICacheInvalidationService<ReviewRepliComment, CommentCacheInfo>
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
        CommentCacheInfo entityInfo)
    {
    }

    public async Task InvalidateDeleteAsync(
        CommentCacheInfo entityInfo)
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
        CommentCacheInfo entityInfo)
    {
        await InvalidateAsync(entityInfo);
    }

    private async Task InvalidateAsync(
        CommentCacheInfo entityInfo)
    {
        await _cache.RemoveAsync(CacheKeys.
             Entity<ReviewComment>(entityInfo.Id));

        await _cache.RemoveByPatternAsync(ReviewRepliCommentCache.
             ByReviewCommentPattern(entityInfo.CommentableEntityId));

        await _cache.RemoveByPatternAsync(ReviewRepliCommentCache.
             ByReviewRepliCommentPattern(entityInfo.Id));
    }

    private async Task<List<PictureCacheInfo>> GetImageInfos(
        long entityId)
    {
        return await _imageRepository
            .GetCacheInfosByOwnerAsync(entityId);
    }
}
