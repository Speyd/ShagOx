using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewRepliComments;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewRepliComments.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Comments.Core.Mapping;
using ShagOxServer.Application.Services.Comments.ReviewRepliComments.Mapping;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewRepliComments.Query;
public class ReviewRepliCommentQueryService
    : BaseQueryService<
        ReviewRepliCommentDto,
        ReviewRepliComment,
        ReviewRepliCommentSearchFilter
        >,
    IReviewRepliCommentQueryService
{
    private readonly IAvatarQueryService _avatarService;
    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public ReviewRepliCommentQueryService(
        IAvatarQueryService avatarService,
        IReviewRepliCommentQueryRepository commentQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(commentQueryRepository, cacheService, settings)
    {
        _avatarService = avatarService;
    }


    public override async Task<ReviewRepliCommentDto> ApplyMapperAsync(
        ReviewRepliComment entity)
    {
        var avatar = entity.Owner.Avatar is null ? null :
            await _avatarService.ApplyMapperAsync(entity.Owner.Avatar);

        var user = CommentCacheMapper
             .ToUserCommentDto(entity, avatar);

        return ReviewRepliCommentMapper.ToDto(entity, user);
    }
}
