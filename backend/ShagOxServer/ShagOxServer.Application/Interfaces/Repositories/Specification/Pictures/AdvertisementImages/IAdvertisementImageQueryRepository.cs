using ShagOxServer.Application.Interfaces.Repositories.Base.Query;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
public partial interface IAdvertisementImageQueryRepository
    : ISearchRepository<AdvertisementImage, ImageSearchFilter>
{
    Task<List<AdvertisementImage>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken = default);

    Task<List<AdvertisementImage>> GetByAdvertisementAsync(
        long advertId,
        CancellationToken cancellationToken = default);

    Task<int> GetNextOrder(
        long advertId,
        int? requestedOrder = null,
        CancellationToken cancellationToken = default);
}
