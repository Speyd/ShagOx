using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification.Images;
public interface IImageQueryRepository
{
    Task<Image?> GetByIdAsync(int id);

    Task<List<Image>> GetByIdsAsync(List<int> ids);


}
