using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Mapping;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Services.Advertisements.AdvertisementVariants.Query;
public class AdvertisementVariantQueryService
    : BaseQueryService<
        AdvertisementVariantDto,
        AdvertisementVariant,
        AdvertisementVariantSearchFilter
        >,
    IAdvertisementVariantQueryService
{
    public AdvertisementVariantQueryService(
        IAdvertisementVariantQueryRepository statusRepository
    )
        : base(statusRepository)
    {
    }


    protected override AdvertisementVariantDto ApplyMapper(
        AdvertisementVariant entity)
    {
        return AdvertisementVariantMapper.ToDto(entity);
    }
}