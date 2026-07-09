using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageRepository
{
    Task<Image?> GetByIdAsync(int id);

    Task AddAsync(Image image);

    Task<bool> UpdateAsync(Image image);

    Task DeleteAsync(Image image);
}
