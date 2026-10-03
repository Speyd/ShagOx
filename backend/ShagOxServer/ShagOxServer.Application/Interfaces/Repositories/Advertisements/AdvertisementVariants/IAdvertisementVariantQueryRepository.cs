using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.AdvertisementVariants;
public interface IAdvertisementVariantQueryRepository
    : IQueryRepository<AdvertisementVariant, 
        AdvertisementVariantSearchFilter>
{
    Task<PagedResult<AdvertisementVariant>> GetByAdvertisementAsync(
        long advertId,
        PaginationParams pagination);

    Task<List<long>> GetIdsByAdvertisementAsync(
        long advertId);
}