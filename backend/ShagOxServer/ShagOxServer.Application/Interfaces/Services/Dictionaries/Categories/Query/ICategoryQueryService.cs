using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
public interface ICategoryQueryService
    : ITranslatableQueryService<CategoryDto, 
        Category, 
        CategorySearchFilter>
{
    Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination);
}