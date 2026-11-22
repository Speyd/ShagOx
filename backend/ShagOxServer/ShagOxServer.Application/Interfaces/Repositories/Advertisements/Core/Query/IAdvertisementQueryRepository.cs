using ShagOxServer.Application.DTOs.Advertisements.Core.Query.Facets;
using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Advertisements;
using ShagOxServer.Domain.Filters.Advertisements;

namespace ShagOxServer.Application.Interfaces.Repositories.Advertisements.Core.Query;
public partial interface IAdvertisementQueryRepository
    : ISearchRepository<Advertisement, AdvertisementSearchFilter>
{
    Task<Advertisement?> GetByIdShortIncludeAsync(
        long id,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<List<Advertisement>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<AdvertisementFacetsDto> GetFacetsAsync(
        long categoryId,
        CancellationToken cancellationToken = default);
}
