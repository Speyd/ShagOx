using ShagOxServer.Application.DTOs.Advertisements.AdvertisementVariants;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Services.Advertisements.AdvertisementVariants.Query;
public interface IAdvertisementVariantQueryService
    : IQueryService<AdvertisementVariantDto, 
        AdvertisementVariantSearchFilter>
{
}