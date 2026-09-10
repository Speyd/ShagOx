using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Query;
public class CategoryTranslationQueryService
    : ICategoryTranslationQueryService
{
    private readonly ICategoryTranslationQueryRepository _categoryRepository;


    public CategoryTranslationQueryService(
        ICategoryTranslationQueryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<Result<CategoryTranslationDto>> GetByIdAsync(
        int id)
    {
        var category = await _categoryRepository
            .GetByIdAsync(id);

        return category.ToResult(CategoryTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var category = await _categoryRepository
            .GetPagedAsync(pagination, language);

        return category.ToResultPaged(CategoryTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var categories = await _categoryRepository
            .GetPagedAsync(pagination);

        return categories.ToResultPaged(CategoryTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryTranslationDto>>> Search(
        CategoryTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var categories = await _categoryRepository
            .Search(filter, pagination);

        return categories.ToResultPaged(CategoryTranslationMapper.ToDto);
    }
}