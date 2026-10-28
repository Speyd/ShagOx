using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
public interface IAdvertisementVariantQueryService
    : IQueryService<AdvertisementVariantDto,
        AdvertisementVariant,
        AdvertisementVariantSearchFilter>
{
}
