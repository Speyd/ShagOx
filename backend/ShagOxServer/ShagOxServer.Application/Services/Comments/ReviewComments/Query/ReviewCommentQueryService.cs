using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Comments.Core.Mapping;
using ShagOxServer.Application.Services.Comments.ReviewComments.Mapping;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Query;
public class ReviewCommentQueryService
    : BaseQueryService<
        ReviewCommentDto,
        ReviewComment,
        ReviewCommentSearchFilter
        >,
    IReviewCommentQueryService
{
    private const int MaxRatingsRequest = 100;

    private readonly IAvatarQueryService _avatarService;
    private readonly IReviewCommentQueryRepository _commentRepository;
    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public ReviewCommentQueryService(
        IAvatarQueryService avatarService,
        IReviewCommentQueryRepository commentQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(commentQueryRepository, cacheService, settings)
    {
        _avatarService = avatarService;
        _commentRepository = commentQueryRepository;
    }


    public async Task<Result<List<ReviewRatingDto>>> GetRatingsAsync(
        IReadOnlyCollection<long> advertisementIds,
        CancellationToken cancellationToken = default)
    {
        var ids = advertisementIds
            .Distinct()
            .Take(MaxRatingsRequest)
            .ToList();

        if (ids.Count == 0)
            return Result<List<ReviewRatingDto>>.Success([]);

        var ratings = await _commentRepository
            .GetRatingsAsync(ids, cancellationToken);

        return Result<List<ReviewRatingDto>>.Success(ratings);
    }


    public override async Task<ReviewCommentDto> ApplyMapperAsync(
        ReviewComment entity)
    {
        var avatar = entity.Owner.Avatar is null ? null :
            await _avatarService.ApplyMapperAsync(entity.Owner.Avatar);

        var user = CommentCacheMapper
            .ToUserCommentDto(entity, avatar);

        return ReviewCommentMapper.ToDto(entity, user);
    }
}
