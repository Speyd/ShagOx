using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryExistsRepository
    : IExistsRepository<Category>
{
    Task<bool> ExistsAsync(
        string code,
        long productTypeId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);

    Task<bool> ExistsByProductTypeAsync(
        long productTypeId,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
