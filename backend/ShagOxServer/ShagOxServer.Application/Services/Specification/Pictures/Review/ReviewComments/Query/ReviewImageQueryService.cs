using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Review.ReviewImages;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Review.ReviewImages.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Mapping;
using ShagOxServer.Domain.Entities.Specification.Pictures.Review;
using ShagOxServer.Domain.Filters.Specification.Pictures.Review;

namespace ShagOxServer.Application.Services.Specification.Pictures.Review.ReviewComments.Query;
public class ReviewImageQueryService
    : BaseQueryService<
        ReviewImageDto,
        ReviewImage,
        ReviewImageSearchFilter
        >,
    IReviewImageQueryService
{
    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;


    public ReviewImageQueryService(
        IReviewImageQueryRepository imageQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(imageQueryRepository, cacheService, settings)
    {
    }


    public override async Task<ReviewImageDto> ApplyMapperAsync(
        ReviewImage entity)
    {
        return ReviewImageMapper.ToDto(entity);
    }
}
