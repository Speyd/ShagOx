using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(int id);

    Task AddAsync(Category category);

    Task<bool> UpdateAsync(Category category);

    Task DeleteAsync(Category category);

}
