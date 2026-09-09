using ShagOxServer.Application.Interfaces.Repositories.Base;
using ShagOxServer.Domain.Entities.Dictionaries;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
public interface ICategoryExistsRepository
    : IExistsRepository<Category>
{
    Task<bool> ExistsAsync(
        string code,
        int productTypeId);

    Task<bool> ExistsByCodeAsync(string code);

    Task<bool> ExistsByProductTypeAsync(int productTypeId);
}