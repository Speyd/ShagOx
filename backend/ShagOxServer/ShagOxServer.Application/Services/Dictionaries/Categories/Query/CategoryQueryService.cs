using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Query;
public class CategoryQueryService : ICategoryQueryService
{
    private readonly ICategoryQueryRepository _repository;

    public CategoryQueryService(
        ICategoryQueryRepository categoryRepository)
    {
        _repository = categoryRepository;
    }

    public async Task<Result<CategoryDto>> GetByIdAsync(
        int id)
    {
        var category = await _repository.GetByIdAsync(id);

        return category.ToResult(CategoryMapper.ToDto);
    }

    public async Task<Result<CategoryDto>> GetByNameAsync(
        string name)
    {
        var category = await _repository.GetByNameAsync(name);

        return category.ToResult(CategoryMapper.ToDto);
    }

    public async Task<Result<List<CategoryDto>>> GetByProductTypeAsync(
        ProductType type)
    {
        var categories = await _repository.GetByProductTypeAsync(type);

        return categories.ToResultList(CategoryMapper.ToDto);
    }

    public async Task<Result<List<CategoryDto>>> Search(
        CategorySearchFilter filter,
        PaginationParams pagination)
    {
        var categories = await _repository.Search(filter, pagination);

        return categories.ToResultList(CategoryMapper.ToDto);
    }
}