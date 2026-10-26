using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewRepliImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Mapping;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewRepliImages.Query;
public class ReviewRepliImageQueryService
    : BaseQueryService<
        ReviewRepliImageDto,
        ReviewRepliImage,
        ReviewRepliImageSearchFilter
        >,
    IReviewRepliImageQueryService
{
    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public ReviewRepliImageQueryService(
        IReviewRepliImageQueryRepository imageQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(imageQueryRepository, cacheService, settings)
    {
    }


    public override async Task<ReviewRepliImageDto> ApplyMapperAsync(
        ReviewRepliImage entity)
    {
        return ReviewRepliImageMapper.ToDto(entity);
    }
}
