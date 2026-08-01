using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.ProductTypes;
public interface IProductTypeRepository
{
    Task<ProductType?> GetByIdAsync(int id);

    void Add(ProductType productType);

    bool Update(ProductType productType);

    void Delete(ProductType productType);
}