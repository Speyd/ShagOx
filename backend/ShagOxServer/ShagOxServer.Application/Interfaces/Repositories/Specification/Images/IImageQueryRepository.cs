using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageQueryRepository
    : IQueryRepository<Image>
{
    Task<List<Image>> GetByIdsAsync(List<int> ids);

    Task<List<Image>> GetByAdvertisementIdAsync(int advertId);

    Task<int> GetNextOrder(int advertId, int? requestedOrder = null);
}