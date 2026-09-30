using Microsoft.Extensions.Options;
using ShagOxServer.Application.Common.Settings.Caches;
using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Caches;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
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
        ICacheService cacheService,
        IOptions<CacheSettings> settings
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