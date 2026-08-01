using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Query;
public class CategoryQueryService : ICategoryQueryService
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;


    public CategoryQueryService(
        ICategoryQueryRepository categoryQueryRepository)
    {
        _categoryQueryRepository = categoryQueryRepository;
    }


    public async Task<Result<CategoryDto>> GetByIdAsync(
        int id)
    {
        var category = await _categoryQueryRepository
            .GetByIdAsync(id);

        return category.ToResult(CategoryMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .GetPagedAsync(pagination);

        return categories.ToResultPaged(CategoryMapper.ToDto);
    }

    public async Task<Result<CategoryDto>> GetByNameAsync(
        string name)
    {
        var category = await _categoryQueryRepository
            .GetByNameAsync(name);

        return category.ToResult(CategoryMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        int productTypeId,
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .GetByProductTypeAsync(productTypeId, pagination);

        return categories.ToResultPaged(CategoryMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryDto>>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .Search(filter, pagination);

        return categories.ToResultPaged(CategoryMapper.ToDto);
    }
}