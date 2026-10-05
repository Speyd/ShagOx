using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
public partial interface IAdvertisementVariantQueryRepository
    : IQueryRepository<AdvertisementVariant,
        AdvertisementVariantSearchFilter>
{
    Task<List<long>> GetIdsByAdvertisementAsync(
        long advertId);
}