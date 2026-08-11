using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryExistsRepository
    : IExistsRepository<Category>
{
    Task<bool> ExistsAsync(
        string name,
        int productTypeId);

    Task<bool> ExistsByNameAsync(string name);

    Task<bool> ExistsByProductTypeAsync(int productTypeId);
}