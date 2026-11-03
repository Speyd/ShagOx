using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeExistsRepository
    : IExistsRepository<ProductType>
{
    Task<bool> ExistsByCodeAsync(
        string code,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null);
}
