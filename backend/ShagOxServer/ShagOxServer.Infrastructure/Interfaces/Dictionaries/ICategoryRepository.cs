using ShagOxServer.Domain.Entities.Account;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries;
public interface ICategoryRepository
{
    Task<Category?> GetByNameAsync(string name);

    Task<Category?> GetByProductTypeAsync(ProductType type);

    Task<Category?> GetByIdAsync(int id);

    Task AddAsync(Category category);

    Task<bool> ExistsNameAsync(string name);

    Task<bool> ExistsProductTypeAsync(ProductType type);

    Task<bool> UpdateAsync(Category category);
}
