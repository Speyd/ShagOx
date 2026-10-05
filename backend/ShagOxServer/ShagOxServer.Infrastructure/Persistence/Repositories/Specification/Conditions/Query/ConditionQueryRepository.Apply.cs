using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Query;
public partial class ConditionQueryRepository
    : QueryRepository<Condition, ConditionSearchFilter>,
      IConditionQueryRepository
{
    protected override IQueryable<Condition> ApplyFilter(
        IQueryable<Condition> query,
        ConditionSearchFilter filter)
    {
        return query.Filter(filter);
    }
}