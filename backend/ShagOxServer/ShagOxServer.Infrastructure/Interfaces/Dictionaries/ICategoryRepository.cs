using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries;
public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);

    Task<Category?> GetByNameAsync(string name);

    Task<List<Category>> GetByProductTypeAsync(ProductType type);


    Task<bool> ExistsIdAsync(int id);

    Task<bool> ExistsNameAsync(string name);

    Task<bool> ExistsProductTypeAsync(ProductType type);


    Task AddAsync(Category category);

    Task<bool> UpdateAsync(Category category);

    Task DeleteAsync(Category category);

}
