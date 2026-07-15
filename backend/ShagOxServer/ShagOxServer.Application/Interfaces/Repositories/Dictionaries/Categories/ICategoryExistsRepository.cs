using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryExistsRepository
{
    Task<bool> ExistsAsync(
        string name,
        ProductType type);

    Task<bool> ExistsByIdAsync(int id);

    Task<bool> ExistsByNameAsync(string name);

    Task<bool> ExistsByProductTypeAsync(ProductType type);
}