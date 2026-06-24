using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Infrastructure.Interfaces.Specification;
public interface IImageRepository
{
    Task<Image?> GetByIdAsync(int id);

    Task AddAsync(Image image);

    Task<bool> UpdateAsync(Image image);
}
