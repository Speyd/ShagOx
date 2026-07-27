using Microsoft.EntityFrameworkCore;
using ShagOxServer.Application.Interfaces.Repositories.Specification.Conditions;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Domain.Filters.Specification.Conditions;
using ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions.Extensions;
using ShagOxServer.SharedKernel.Abstractions.Paginations;

namespace ShagOxServer.Infrastructure.Persistence.Repositories.Specification.Conditions;
public class ConditionQueryRepository : BaseRepository, IConditionQueryRepository
{
    public ConditionQueryRepository(AppDbContext db)
        : base(db)
    { }


    public async Task<Condition?> GetByIdAsync(int id)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedResult<Condition>> GetPagedAsync(
       PaginationParams pagination)
    {
        return await _db.Conditions
            .ToPagedResultAsync(pagination);
    }

    public async Task<Condition?> GetByNameAsync(string name)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Name == name);
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