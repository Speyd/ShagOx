using ShagOxServer.Domain.Entities.Specification;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Images;
public interface IImageRepository
{
    Task<Image?> GetByIdAsync(int id);

    void Add(Image image);

    bool Update(Image image);

    void Delete(Image image);
}