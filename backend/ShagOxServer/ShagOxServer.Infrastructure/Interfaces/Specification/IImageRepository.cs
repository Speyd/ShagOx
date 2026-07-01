using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification;
public interface IImageRepository
{
    Task AddAsync(Image image);

    Task<bool> UpdateAsync(Image image);

    Task DeleteAsync(Image image);


    Task<Image?> GetByIdAsync(int id);

    Task<List<Image>> GetByIdsAsync(List<int> ids);
}
