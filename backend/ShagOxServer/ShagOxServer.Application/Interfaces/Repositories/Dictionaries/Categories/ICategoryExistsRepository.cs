using ShagOxServer.Domain.Entities.Dictionaries.Enum;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryExistsRepository
{
    Task<bool> ExistsIdAsync(int id);

    Task<bool> ExistsNameAsync(string name);

    Task<bool> ExistsProductTypeAsync(ProductType type);
}
