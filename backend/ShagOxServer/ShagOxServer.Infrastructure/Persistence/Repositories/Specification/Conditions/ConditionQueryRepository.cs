using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Dictionaries.Attributes.AttributeDefinitions.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Extensions;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
public class ConditionQueryRepository 
    : QueryRepository<Condition, ConditionSearchFilter>, 
      IConditionQueryRepository
{
    public ConditionQueryRepository(AppDbContext db)
        : base(db)
    { }


    protected override IQueryable<Condition> ApplyFilter(
        IQueryable<Condition> query,
        ConditionSearchFilter filter)
    {
        return query.Filter(filter);
    }

    public async Task<Condition?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x =>
                x.Code == identificator);
    }
}