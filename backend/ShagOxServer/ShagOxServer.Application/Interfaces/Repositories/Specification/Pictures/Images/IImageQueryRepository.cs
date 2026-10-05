using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification.Pictures;
using ShagOxServer.Domain.Filters.Specification.Pictures.Images;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Pictures.Images;
public partial interface IImageQueryRepository
    : IQueryRepository<Image, ImageSearchFilter>
{
    Task<List<Image>> GetByIdsAsync(
        List<long> ids);

    Task<List<Image>> GetByAdvertisementAsync(
        long advertId);

    Task<int> GetNextOrder(
        long advertId,
        int? requestedOrder = null);
}