using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Auth.Users.Core.Query;
using ShagOxServer.Application.DTOs.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Repositories.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Comments.ReviewComments.Query;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Avatars.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Comments.ReviewComments.Mapping;
using ShagOxServer.Domain.Entities.Comments;
using ShagOxServer.Domain.Filters.Comments;

namespace ShagOxServer.Application.Services.Comments.ReviewComments.Query;
public class ReviewCommentQueryService
    : BaseQueryService<
        ReviewCommentDto,
        ReviewComment,
        ReviewCommentSearchFilter
        >,
    IReviewCommentQueryService
{
    private readonly IAvatarQueryService _avatarService;
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
    }


    public override async Task<ReviewCommentDto> ApplyMapperAsync(
        ReviewComment entity)
    {
        var avatar = entity.Owner.Avatar is null ? null :
            await _avatarService.ApplyMapperAsync(entity.Owner.Avatar);

        var user = new UserCommentDto(
            entity.Owner.Id,
            entity.Owner.FirstName,
            entity.Owner.LastName,
            entity.Owner.UserName,
            avatar,
            entity.Owner.CityId,
            entity.Owner.LastSeenAt);

        return ReviewCommentMapper.ToDto(entity, user);
    }
}
