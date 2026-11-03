using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Base.Enum;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.DbContexts.Replica;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Extensions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base.Query;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Query;
public partial class ConditionQueryRepository 
    : SearchRepository<Condition, ConditionSearchFilter>, 
      IConditionQueryRepository
{
    public ConditionQueryRepository(
        ReplicaDbContext db)
        : base(db)
    { }


    public async Task<Condition?> GetByIdentificatorAsync(
        string identificator,
        long? parentId = null,
        CancellationToken cancellationToken = default,
        EntityStatus? status = null)
    {
        return await _db.Conditions
            .WithStatusFilter(status)
            .FirstOrDefaultAsync(x =>
                x.Code == identificator,
                cancellationToken);
    }
}
