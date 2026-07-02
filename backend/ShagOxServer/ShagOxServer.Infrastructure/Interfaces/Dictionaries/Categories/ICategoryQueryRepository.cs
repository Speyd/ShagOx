using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
public interface ICategoryQueryRepository
{
    Task<Category?> GetByIdAsync(int id);

    Task<Category?> GetByNameAsync(string name);

    Task<List<Category>> GetByProductTypeAsync(ProductType type);

    Task<List<Category>> SearchByName(
       string name,
       PaginationParams pagination);
}
