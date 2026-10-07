using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants.Query;
public partial interface IAdvertisementVariantQueryRepository
    : ISearchRepository<AdvertisementVariant, 
        AdvertisementVariantSearchFilter>
{
    Task<PagedResult<AdvertisementVariant>> GetByAdvertisementAsync(
        long advertId,
        PaginationParams pagination);
}
