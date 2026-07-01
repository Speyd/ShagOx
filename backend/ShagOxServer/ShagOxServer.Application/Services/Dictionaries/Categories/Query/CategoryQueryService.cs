using ShagOxServer.Application.Common.Results;
using ShagOxServer.Application.Common.Results.Extensions;
using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Enum;
using ShagOxServer.Infrastructure.Interfaces.Dictionaries.Categories;


namespace ShagOxServer.Application.Services.Dictionaries.Categories.Query;
public class CategoryQueryService : ICategoryQueryService
{
    private readonly ICategoryRepository _repository;

    public CategoryQueryService(
        ICategoryRepository categoryRepository)
    {
        _repository = categoryRepository;
    }

    public async Task<Result<CategoryDto>> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category is null)
            return Result<CategoryDto>.NotFound("Category");

        return category.ToResult(CategoryMapper.ToDto);
    }

    public async Task<Result<CategoryDto>> GetByNameAsync(string name)
    {
        var category = await _repository.GetByNameAsync(name);
        if (category is null)
            return Result<CategoryDto>.NotFound("Category");

        return category.ToResult(CategoryMapper.ToDto);
    }

    public async Task<Result<List<CategoryDto>>> GetByProductTypeAsync(
        ProductType type)
    {
        var categories = await _repository.GetByProductTypeAsync(type);
        if (categories is null || !categories.Any())
            return Result<List<CategoryDto>>.NotFound("Category");

        return categories.ToResultList(CategoryMapper.ToDto);
    }
}
