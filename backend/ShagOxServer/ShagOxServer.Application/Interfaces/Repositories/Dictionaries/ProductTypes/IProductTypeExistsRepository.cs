using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeExistsRepository
    : IExistsRepository<ProductType>
{
    Task<bool> ExistsByNameAsync(string name);
}