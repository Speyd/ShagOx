using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Specification.Pictures.Images.Query;
using ShagOxServer.Application.Services.Base.Query;
using ShagOxServer.Application.Services.Specification.Pictures.Images.Mapping;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Services.Specification.Pictures.Images.Query;
public class AdvertisementImageQueryService 
    : BaseQueryService<
        ImageDto,
        AdvertisementImage,
        ImageSearchFilter
        >,
    IAdvertisementImageQueryService
{
    protected override bool CacheBySearch => false;

    protected override bool CacheByPaged => false;

    public AdvertisementImageQueryService(
        IAdvertisementImageQueryRepository imageQueryRepository,
        IDataCacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(imageQueryRepository, cacheService, settings)
    {
    }


    public override async Task<ImageDto> ApplyMapperAsync(
        AdvertisementImage entity)
    {
        return AdvertisementImageMapper.ToDto(entity);
    }
}
