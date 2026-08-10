using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Base;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
public interface ICategoryQueryService
    : IQueryService<CategoryDto>
{
    Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        int productTypeId,
        PaginationParams pagination);

    Task<Result<PagedResult<CategoryDto>>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination);
}