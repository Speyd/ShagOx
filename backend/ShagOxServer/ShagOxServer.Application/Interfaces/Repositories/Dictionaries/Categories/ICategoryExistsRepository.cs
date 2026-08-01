using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryExistsRepository
{
    Task<bool> ExistsAsync(
        string name,
        int productTypeId);

    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);

    Task<bool> ExistsByProductTypeAsync(int productTypeId);
}