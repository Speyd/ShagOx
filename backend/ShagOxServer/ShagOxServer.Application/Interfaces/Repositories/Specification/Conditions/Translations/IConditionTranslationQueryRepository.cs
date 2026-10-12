using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Entities.Specification.Translations;
using ShagOxServer.Domain.Filters.Specification.Conditions.Translations;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions.Translations;
public interface IConditionTranslationQueryRepository
    : ISearchTranslationRepository<Condition,
        ConditionTranslation, 
        ConditionTranslationSearchFilter>
{
}
