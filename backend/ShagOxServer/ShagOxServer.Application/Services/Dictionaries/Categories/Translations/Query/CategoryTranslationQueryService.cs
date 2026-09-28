using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
using ShagOxServer.Application.Services.Base.Translations;
using ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Mapping;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Application.Services.Dictionaries.Categories.Translations.Query;
public class CategoryTranslationQueryService
    : BaseTranslationQueryService<
        CategoryTranslationDto,
        CategoryTranslation,
        CategoryTranslationSearchFilter
        >,
    ICategoryTranslationQueryService
{
    public CategoryTranslationQueryService(
        ICategoryTranslationQueryRepository categoryRepository
    )
        : base(categoryRepository)
    {
    }

    protected override async Task<CategoryTranslationDto> ApplyMapperAsync(
        CategoryTranslation entity)
    {
        return CategoryTranslationMapper.ToDto(entity);
    }
}