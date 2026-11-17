using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Catalog;
using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants.Query.Core;
using ShagOxServer.Application.Interfaces.Services.Base.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
public interface IAdvertisementVariantQueryService
    : IQueryService<AdvertisementVariantDto,
        AdvertisementVariant,
        AdvertisementVariantSearchFilter>
{
    Task<AdvertisementVariantCatalogDto> ApplyCatalogMapperAsync(
      AdvertisementVariant entity);
}
