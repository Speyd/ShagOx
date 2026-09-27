using ShagOxServer.Application.DTOs.Dictionaries.Categories.Translations;
using ShagOxServer.Application.Interfaces.Services.Base.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Application.Interfaces.Services.Dictionaries.Categories.Translations.Query;
public interface ICategoryTranslationQueryService
    : IQueryTranslationService<CategoryTranslationDto, 
        CategoryTranslationSearchFilter>
{
}