using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Mapping;
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
        var region = await _categoryRepository
            .GetByIdAsync(id);

        return region.ToResult(CategoryTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryTranslationDto>>> GetPagedAsync(
        PaginationParams pagination,
        string language)
    {
        var regions = await _categoryRepository
            .GetPagedAsync(pagination, language);

        return regions.ToResultPaged(CategoryTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryTranslationDto>>> GetPagedAsync(
        PaginationParams pagination)
    {
        var regions = await _categoryRepository
            .GetPagedAsync(pagination);

        return regions.ToResultPaged(CategoryTranslationMapper.ToDto);
    }

    public async Task<Result<PagedResult<CategoryTranslationDto>>> Search(
        CategoryTranslationSearchFilter filter,
        PaginationParams pagination)
    {
        var regions = await _categoryRepository
            .Search(filter, pagination);

        return regions.ToResultPaged(CategoryTranslationMapper.ToDto);
    }
}