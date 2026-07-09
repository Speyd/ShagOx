using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageQueryRepository
{
    Task<Image?> GetByIdAsync(int id);

    Task<List<Image>> GetByIdsAsync(List<int> ids);

    Task<int> GetNextOrder(int advertId, int? requestedOrder = null);
}