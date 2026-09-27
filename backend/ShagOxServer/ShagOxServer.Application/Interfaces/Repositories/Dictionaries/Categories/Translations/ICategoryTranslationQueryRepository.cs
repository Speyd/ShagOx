using ShagOxServer.Application.Interfaces.Repositories.Base.Translations;
using ShagOxServer.Domain.Entities.Dictionaries.Translations;
using ShagOxServer.Domain.Filters.Dictionaries.Categories.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Dictionaries.Categories.Translations;
public interface ICategoryTranslationQueryRepository
    : ITranslationQueryRepository<CategoryTranslation, 
        CategoryTranslationSearchFilter>
{
}