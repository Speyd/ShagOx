using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Dictionaries;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
public interface ICategoryTranslationQueryRepository
    : ISearchTranslationRepository<Category,
        CategoryTranslation, 
        CategoryTranslationSearchFilter>
{
}
