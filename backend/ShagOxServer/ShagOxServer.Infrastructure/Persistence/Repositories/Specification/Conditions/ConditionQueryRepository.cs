using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.DbContexts;
using ShagOxServer.Infrastructure.Persistence.Repositories.Base;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
public class ConditionQueryRepository 
    : QueryRepository<Condition>, 
      IConditionQueryRepository
{
    public ConditionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Condition?> GetByCodeAsync(
        string code)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Code == code);
    }

    public async Task<PagedResult<Condition>> Search(
       ConditionSearchFilter filter,
       PaginationParams pagination)
    {
        return await _db.Conditions
            .Filter(filter)
            .ToPagedResultAsync(pagination);
    }
}