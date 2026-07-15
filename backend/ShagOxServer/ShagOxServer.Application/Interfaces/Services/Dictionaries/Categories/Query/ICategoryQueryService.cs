using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
public interface ICategoryQueryService
{
    Task<Result<CategoryDto>> GetByIdAsync(int id);

    Task<Result<CategoryDto>> GetByNameAsync(string name);

    Task<Result<List<CategoryDto>>> GetByProductTypeAsync(
        ProductType type);

    Task<Result<List<CategoryDto>>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination);
}