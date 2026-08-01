using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
public interface ICategoryQueryService
{
    Task<Result<CategoryDto>> GetByIdAsync(int id);

    Task<Result<PagedResult<CategoryDto>>> GetPagedAsync(
        PaginationParams pagination);

    Task<Result<CategoryDto>> GetByNameAsync(string name);

    Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        int productTypeId,
        PaginationParams pagination);

    Task<Result<PagedResult<CategoryDto>>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination);
}