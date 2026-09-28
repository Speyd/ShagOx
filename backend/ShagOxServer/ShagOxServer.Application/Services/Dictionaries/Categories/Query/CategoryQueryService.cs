using ShagOxServer.Application.DTOs.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Base;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Categories.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Filters.Dictionaries.Categories;
using ShagOxServer.SharedKernel.Abstractions.Paginations;
using ShagOxServer.SharedKernel.Abstractions.Results;
using ShagOxServer.SharedKernel.Abstractions.Results.Extensions;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Query;
public class CategoryQueryService 
    : BaseTranslatableQueryService<
        CategoryDto,
        Category,
        CategorySearchFilter
        >,
    ICategoryQueryService
{
    private readonly ICategoryQueryRepository _categoryQueryRepository;

    private readonly ICategoryTranslationQueryRepository _translationRepository;

    private readonly ILanguageProvider _language;


    public CategoryQueryService(
        ICategoryQueryRepository categoryQueryRepository,
        ICategoryTranslationQueryRepository translationRepository,
        ILanguageProvider language
    )
        : base(categoryQueryRepository)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _translationRepository = translationRepository;
        _language = language;
    }


    public override async Task<CategoryDto> ApplyMapperAsync(
        Category entity)
    {
        var translation = await _translationRepository
            .GetByIdentificatorAsync(entity.Code, _language.Language);

        return CategoryMapper.ToDto(entity, translation?.Name);
    }

    public async Task<Result<PagedResult<CategoryDto>>> GetByProductTypeAsync(
        long productTypeId,
        PaginationParams pagination)
    {
        var categories = await _categoryQueryRepository
            .GetByProductTypeAsync(productTypeId, pagination);

        return await categories.ToResultPagedAsync(ApplyMapperAsync);
    }
}