using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageQueryRepository
{
    Task<Image?> GetByIdAsync(int id);

    Task<List<Image>> GetPagedAsync(
        PaginationParams pagination);

    Task<List<Image>> GetByIdsAsync(List<int> ids);

    Task<List<Image>> GetByAdvertisementIdAsync(int advertId);

    Task<int> GetNextOrder(int advertId, int? requestedOrder = null);
}