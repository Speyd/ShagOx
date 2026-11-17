using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Options.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations.Query;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Query;
public class CategoryTranslationQueryService
    : BaseTranslationQueryService<
        CategoryTranslationDto,
        Category,
        CategoryTranslation,
        CategoryTranslationSearchFilter
        >,
    ICategoryTranslationQueryService
{
    public CategoryTranslationQueryService(
        ICategoryTranslationQueryRepository categoryRepository,
        IDataCacheService cacheService,
        IOptions<CacheOptions> settings
    )
        : base(categoryRepository, cacheService, settings)
    {
    }

    public override async Task<CategoryTranslationDto> ApplyMapperAsync(
        CategoryTranslation entity)
    {
        return CategoryTranslationMapper.ToDto(entity);
    }
}
