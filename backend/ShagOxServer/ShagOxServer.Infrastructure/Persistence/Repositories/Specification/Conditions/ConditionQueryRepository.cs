using Microsoft.EntityFrameworkCore;
using ShagOxServer.Domain.Entities.Specification;
using ShagOxServer.Infrastructure.Interfaces.Specification.Conditions;
using ShagOxServer.SharedKernel.Paginations;

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

    public async Task<Condition?> GetByNameAsync(string name)
    {
        return await _db.Conditions
            .FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<Condition>> SearchByName(
       string name,
       PaginationParams pagination)
    {
        return await _db.Conditions
            .Where(x => x.Name.Contains(name))
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync();
    }
}
