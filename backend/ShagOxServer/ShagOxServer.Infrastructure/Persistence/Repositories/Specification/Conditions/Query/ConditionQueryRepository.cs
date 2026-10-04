using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Query;
public partial class ConditionQueryRepository 
    : QueryRepository<Condition, ConditionSearchFilter>, 
      IConditionQueryRepository
{
    public ConditionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Condition?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x =>
                x.Code == identificator);
    }
}