using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageRepository
{
    Task<Image?> GetByIdAsync(int id);

    Task AddAsync(Image image);

    bool UpdateAsync(Image image);

    void DeleteAsync(Image image);
}
