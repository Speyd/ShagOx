using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Providers;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Caches.Keys.Dictionaries;
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


    public CategoryQueryService(
        ICategoryQueryRepository categoryQueryRepository,
        ICategoryTranslationQueryRepository translationRepository,
        ILanguageProvider language,
        ICacheService cacheService,
        IOptions<CacheSettings> settings
    )
        : base(categoryQueryRepository, language, cacheService, settings)
    {
        _categoryQueryRepository = categoryQueryRepository;
        _translationRepository = translationRepository;
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
        var cacheKey = CategoryCache.ByProductType(
           productTypeId,
           _language.Language,
           pagination.Page,
           pagination.PageSize);

        return await _cache.GetOrCreateAsync(
            cacheKey,
            async () =>
            {
                var categories = await _categoryQueryRepository
                    .GetByProductTypeAsync(productTypeId, pagination);

                return await categories.ToResultPagedAsync(
                    ApplyMapperAsync);
            },
            _settings.KeyExpiration
        );
    }
}