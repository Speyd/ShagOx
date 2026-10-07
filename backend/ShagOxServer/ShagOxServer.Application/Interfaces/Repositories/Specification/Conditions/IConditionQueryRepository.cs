using ShagOxServer.Application.Interfaces.Repositories.Base.Translations.Query;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;

namespace ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
public interface IConditionQueryRepository
    : ISearchTranslatableRepository<Condition,
        ConditionSearchFilter>
{
}
